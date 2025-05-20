using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataRelay : MonoBehaviour
{
    public int money; //�����p�̕ϐ�
    public int maintain; //�ێ���p�̕ϐ�
    public int year; //�N�̕ϐ�
    public int week; //�T�̕ϐ�
    public int user; //���[�U�[���̕ϐ�
    public int famous; //�l�C�x�̕ϐ�
    public int popular;//�m���x�̕ϐ�

    public int server;//�T�[�o�[���x���p�̕ϐ�
    public int debug;//�f�o�b�O�p�̕ϐ�
    public int sns;//�L���p�̕ϐ�

    //�V���O���g���p�ϐ�
    private static DataRelay dr;
    //�V���O���g������
    public static DataRelay Dr
    {
        get
        {
            if(dr == null)
            {
                dr = (DataRelay)FindObjectOfType(typeof(DataRelay));
            }
            return dr;
        }
    }
    private void Awake()
    {
        if(this != dr) { Destroy(this.gameObject); return; }//�����Ă���Ȃ炱��Gameobject�������B
    }
}
