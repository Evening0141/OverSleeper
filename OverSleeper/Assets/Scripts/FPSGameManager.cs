using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPSGameManager : MonoBehaviour
{
    #region field
    private const float _TIME = 3.0f; // ���Ԑݒ�
    private float _time = _TIME;      // ���ԃZ�b�g
    private bool isGame = true;       // ���ʊm���̃N�[���^�C���^�C�~���O
    private bool canCheat = false;    // �`�[�g�t�^

    [System.Serializable]
    public class PlayerSlot
    {
        public GameObject spawnPoint;         // �����ꏊ
        public GameObject playerPrefab;       // �v���C���[�v���n�u
        public Image charaImg;                // �L�����A�C�R��
        public Text nameText;                 // �v���C���[���\��

        public GameObject playerObj;          // ����
        public Character character;           // �X�N���v�g�Q��

        public bool IsDown => character != null && character.IsDown;
    }

    [SerializeField] private List<PlayerSlot> playerSlots = new List<PlayerSlot>();
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
        RandCheat();

        foreach (var slot in playerSlots)
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
            int index = Random.Range(0, playerSlots.Count);
            playerSlots[index].character.UseCheat();
        }

        // ���̃X�C�b�`������
        act = GameAct.MAIN;
    }

    // ����̓L�����O�̂Ȃ̂Ŗ��͂Ȃ���������
    // �̂��̂��������ꍇ�������x�Ŋ뜜���ׂ�
    // ���S�����m����
    private void PlayerActive()
    {
        for (int i = playerSlots.Count - 1; i >= 0; i--)
        {
            var slot = playerSlots[i];
            if (slot.IsDown)
            {
                slot.charaImg.color = Color.red;
                slot.playerObj.transform.position = new Vector3(0, -255, 0);
                playerSlots.RemoveAt(i); // ���S�L���������X�g����폜
            }
        }

        // �����v���C���[������l���̓[���ɂȂ����Ƃ������L���O��
        if (1>=playerSlots.Count)
        {
            act = GameAct.RANK;
        }
    }

    private void RankingDisp()
    {

    }

    // �`�[�g���I
    private void RandCheat()
    {
        if (canCheat)
        {
            Debug.Log("�`�[�g�g�p�҂����܂�");
            return;
        }
        // ��: 90% �̊m���� false�A10% �̊m���� true
        canCheat = Random.value < 0.1f;
        Debug.Log(canCheat + "�`�[�g�̗L��");
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
