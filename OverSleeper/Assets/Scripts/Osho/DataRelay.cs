using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataRelay : MonoBehaviour
{
    public int money = 0; //�����p�̕ϐ�
    public int maintain = 0; //�ێ���p�̕ϐ�
    public int year = 0; //�N�̕ϐ�
    public int week = 0; //�T�̕ϐ�
    public int user = 0; //���[�U�[���̕ϐ�
    public int famous = 0; //�l�C�x�̕ϐ�
    public int popular = 0;//�m���x�̕ϐ�

    #region Level
    public int server = 1;//�T�[�o�[���x���p�̕ϐ�
    public int debug = 1;//�f�o�b�O�p�̕ϐ�
    public int sns = 1;//�L���p�̕ϐ�
    #endregion

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
        if(dr == null)
        {
            dr = this;
        }
        if(this != dr) { Destroy(this.gameObject); return; }//�����Ă���Ȃ炱��Gameobject�������B
    }
}
