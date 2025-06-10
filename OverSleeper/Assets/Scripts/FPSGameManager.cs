using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSGameManager : MonoBehaviour
{
    private const float _TIME = 3.0f; // ���Ԑݒ�
    private float _time = _TIME;
    private bool isGame = true; // ���ʊm���̃N�[���^�C���^�C�~���O
    private bool canCheat = false; // �`�[�g�t�^
    
    [SerializeField] GameObject[] spawnObj;  // �����ꏊ
    [SerializeField] GameObject[] playerObj; // �v���C���[
    // Character�X�N���v�g�̃��C��������
    // �v���C���[�I�u�W�F�N�g���̂��e���̃A�b�v�f�[�g�ŏ���
    Character[] characterScr;                // �L�����N�^�[�̃X�N���v�g�i�z��̏������͎g�p����ۂɃ��\�b�h�ōs���j
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
        if (!isGame) {
            EndCoolTime();
            return; 
        }
        else if(isGame)
        {
            switch(act)
            {
                case GameAct.SPAWN:
                    PlayerSet();
                    break;
                case GameAct.MAIN:
                    PlayerActive();
                    break;
                case GameAct.RANK:
                    break;
            }
        }
    }

    // ��������
    private void PlayerSet()
    {
        // �z��̏�����
        characterScr = new Character[playerObj.Length];
        // ���I
        RandCheat();
        // �������[�v
        for(int i=0;i<playerObj.Length;i++)
        {
            GameObject obj = Instantiate(playerObj[i], spawnObj[i].transform.position, Quaternion.identity);
            characterScr[i] = obj.GetComponent<Character>();
            // �����L�����̏��������s��
            characterScr[i].PlayerStatus();
        }
        // true���������ꍇ�͒N���ɕt�^
        if (canCheat) {
            // �����_���Ƀv���C���[��I��
            int index = Random.Range(0, characterScr.Length);
            // �`�[�g�t�^
            characterScr[index].UseCheat();
        }
        // ���̃X�C�b�`������
        act = GameAct.MAIN;
    }

    // ���S�����m����
    private void PlayerActive()
    {
        
    }

    // �`�[�g���I
    private void RandCheat()
    {
        if (canCheat) {Debug.Log("�`�[�g�g�p�҂����܂�"); return; }
        // ��: 90% �̊m���� false�A10% �̊m���� true
        canCheat = Random.value < 0.1f;
        Debug.Log(canCheat+"�f�o�b�O����");
    }

    // �ҋ@�v�Z
    private void EndCoolTime()
    {
        _time -= Time.deltaTime;
        if (_time <= 0)
        {
            isGame = true; // ��������
            _time = _TIME; // �ăZ�b�g
        }
    }
}
