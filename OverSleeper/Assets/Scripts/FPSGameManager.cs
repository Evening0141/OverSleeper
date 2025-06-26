using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPSGameManager : MonoBehaviour
{
    #region field
    // �Q�[���^�C��
    [SerializeField] Text timerText;  // ���ԕ\��
    private const float _GAMETIME = 45.0f;
    private float _GameTime = _GAMETIME;

    // �N�[���^�C��
    private const float _TIME = 3.0f; // ���Ԑݒ�
    private float _time = _TIME;      // ���ԃZ�b�g
    private bool isGame = true;       // ���ʊm���̃N�[���^�C���^�C�~���O
    private bool canCheat = false;    // �`�[�g�t�^

    [Header("������\���̃I�u�W�F�N�g"),SerializeField] GameObject readyObj;
    [Header("�����N�\��"),SerializeField] Text Rank;

    string winUser = "";  // �����v���C���[�̖��O

    [System.Serializable]
    public class PlayerSlot
    {
        public Sprite CharaSp;                // �摜
        public GameObject spawnPoint;         // �����ꏊ
        public GameObject playerPrefab;       // �v���C���[�v���n�u
        public Image charaImg;                // �L�����A�C�R��
        public Text nameText;                 // �v���C���[���\��

        public GameObject playerObj;          // ����
        public Character character;           // �X�N���v�g�Q��

        public bool IsDown => character != null && character.IsDown;
    }

    [SerializeField] private List<PlayerSlot> playerSlots = new List<PlayerSlot>();
    private List<PlayerSlot> activePlayerSlots = new List<PlayerSlot>();
    #endregion

    // �X�C�b�`�����̔��f
    private enum GameAct
    {
        SPAWN, // �v���C���[�̐���
        MAIN,  // �Q�[���J�n
        RANK,  // ���ʕ\��
    }

    // �����̓v���C���[�̃X�|�[������
    private GameAct act = GameAct.SPAWN;

    private void Update()
    {
        FPSUpdate();
    }

    // ���C�������i���C���}�l�[�W���[�ł̌Ăяo���j
    public void FPSUpdate()
    {
        // �N�[���^�C�����Ȃ炱�̂܂ܓ��������ҋ@
        if (!isGame)
        {
            EndCoolTime();
            return;
        }
        else if (isGame)
        {
            switch (act)
            {
                case GameAct.SPAWN: // �v���C���[�����⏉����
                    PlayerSet();
                    break;
                case GameAct.MAIN:  // ���������ǂ���
                    PlayerActive();
                    break;
                case GameAct.RANK:  // �����L���O�ɕ\��
                    RankingDisp();
                    break;
            }
        }
    }

    // ��������
    private void PlayerSet()
    {
        // ������
        winUser = "";

        // �p�l����\��
        readyObj.SetActive(false);

        // ����
        activePlayerSlots = new List<PlayerSlot>(playerSlots); 

        // ���I
        RandCheat();

        // ���ԃZ�b�g
        _GameTime = _GAMETIME;

        foreach (var slot in activePlayerSlots)
        {
            GameObject obj = Instantiate(slot.playerPrefab, slot.spawnPoint.transform.position, Quaternion.identity);
            Character chara = obj.GetComponent<Character>();
            chara.PlayerStatus(); // ������

            slot.playerObj = obj;
            slot.character = chara;
            slot.nameText.text = chara.GetUserID;
            slot.charaImg.color = Color.white; // ���S�L�������ƐԂɕύX����Ă���̂�
        }

        // true���������ꍇ�͒N���ɕt�^
        if (canCheat)
        {
            int index = Random.Range(0, activePlayerSlots.Count);
            activePlayerSlots[index].character.UseCheat();
        }

        // ���̃X�C�b�`������
        act = GameAct.MAIN;
    }

    private void PlayerActive()
    {
        // ����̓L�����O�̂Ȃ̂Ŗ��͂Ȃ���������
        // �̂��̂��������ꍇ�������x�Ŋ뜜���ׂ�
        // ���S�����m����
        for (int i = activePlayerSlots.Count - 1; i >= 0; i--)
        {
            var slot = activePlayerSlots[i];
            if (slot.IsDown)
            {
                slot.charaImg.color = Color.red;
                Destroy(slot.playerObj);
                activePlayerSlots.RemoveAt(i); // ���S�L���������X�g����폜
            }
        }
        // ���ԏ���
        if (_GameTime > 0)
        {
            _GameTime -= Time.deltaTime;
            if (_GameTime <= 0)
            {
                act = GameAct.RANK;
                timerText.text= "�I���I";
                return;
            }
        }
        int min = Mathf.FloorToInt(_GameTime / 60);
        int sec = Mathf.FloorToInt(_GameTime % 60);
        float miri = _GameTime % 1.0f;
        // �e�L�X�g�ύX
        timerText.text = string.Format("{0:00}:{1:00}:{2:00}", min, sec, Mathf.FloorToInt(miri * 100));

        // �����v���C���[������l���̓[���ɂȂ����Ƃ�
        // �Q�[�����Ԃ��I���̏ꍇ�����L���O��
        if (1>=activePlayerSlots.Count||_GameTime<=0)
        {
            if (activePlayerSlots.Count == 1)
            {
                winUser = activePlayerSlots[0].nameText.text; // ���O�ۊ�
            }
            act = GameAct.RANK;
            return;
        }
    }

    private void RankingDisp()
    {
        readyObj.SetActive(true);
        isGame = false;
        // �����v���C���[��\��
        if (activePlayerSlots.Count == 1)
        {
            Rank.text = winUser + " WIN";
        }
        else�@// ���ԏI���̏ꍇ
        {
            Rank.text = "DRAW";
        }
        for (int i = activePlayerSlots.Count - 1; i >= 0; i--)
        {
            var slot = activePlayerSlots[i];
            Destroy(slot.playerObj);
            activePlayerSlots.RemoveAt(i); // �S�č폜
        }
    }

    // �`�[�g���I
    private void RandCheat()
    {
        // �f�[�^�����炤
        canCheat = DataRelay.Dr.IsCheat;

        if (canCheat)
        {
            Debug.Log("�`�[�g�g�p�҂����܂�");
            return;
        }
        // ��: 90% �̊m���� false�A10% �̊m���� true
        canCheat = Random.value < 0.1f;
        Debug.Log(canCheat + "�`�[�g�̗L��");

        // ���I��ɒ��p�ɏ��𑗂�
        DataRelay.Dr.IsCheat = canCheat;
    }

    // �ҋ@�v�Z
    private void EndCoolTime()
    {
        _time -= Time.deltaTime;
        if (_time <= 0)
        {
            act = GameAct.SPAWN; // ���̃o�g������
            isGame = true; // ��������
            _time = _TIME; // �ăZ�b�g
        }
    }
}
