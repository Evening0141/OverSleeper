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
    public int popularity; //�l�C�x�̕ϐ�
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
