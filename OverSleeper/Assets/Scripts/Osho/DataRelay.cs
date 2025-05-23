using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataRelay : MonoBehaviour
{
    private int money = 1000; //�����p�̕ϐ�
    private int maintain = 0; //�ێ���p�̕ϐ�
    private int year = 0; //�N�̕ϐ�
    private int week = 0; //�T�̕ϐ�
    private int user = 0; //���[�U�[���̕ϐ�
    private int famous = 0; //�l�C�x�̕ϐ�
    private int popular = 0;//�m���x�̕ϐ�

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
    public int Money //Money(����)�p�̃Q�b�^�[�Z�b�^�[
    {
        get => money;
        set
        {
            Debug.Log("money"+ money);
            money = value;
        }
    }
    public int Maintain�@//Maintain(�ێ���)�p�̃Q�b�^�[�Z�b�^�[
    {
        get => maintain;
        set
        {
            Debug.Log("maintain" + maintain);
            maintain = value;
        }
    }
    public int Year�@//Year(�N)�p�̃Q�b�^�[�Z�b�^�[
    {
        get => year;
        set
        {
            Debug.Log("year" + year);
            year = value;
        }
    }
    public int Week //Weak(�T)�p�̃Q�b�^�[�Z�b�^�[
    {
        get => week;
        set
        {
            Debug.Log("week" +week);
            week = value;
        }
    }
    public int User //User(���[�U�[)�p�̃Q�b�^�[�Z�b�^�[
    {
        get => user;
        set
        {
            Debug.Log("user" +user);
            user = value;
        }
    }
    public int Famous //Famous(�l�C�x)�p�̃Q�b�^�[�Z�b�^�[
    {
        get => famous;
        set
        {
            Debug.Log("famous" +famous);
            famous = value;
        }
    }
    public int Popular //Popular(�m���x)�p�̃Q�b�^�[�Z�b�^�[
    {
        get => popular;
        set
        {
            Debug.Log("popular" +popular);
            popular = value;
        }
    }
    private void Awake()
    {
        if (dr != null && dr != this)
        {
            Destroy(this.gameObject); // �d����h�~
        }
        else
        {
            dr = this;
            DontDestroyOnLoad(this.gameObject); // �V�[�����܂����ł��c��
        }
    }

}
