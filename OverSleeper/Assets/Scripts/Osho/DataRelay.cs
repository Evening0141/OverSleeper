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
    private int _server = 1;//�T�[�o�[���x���p�̕ϐ�
    private int _debug = 1;//�f�o�b�O�p�̕ϐ�
    private int _sns = 1;//�L���p�̕ϐ�
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
    public int Server //�ݔ����x��(�T�[�o�[)�p�̃Q�b�^�[�Z�b�^�[
    {
        get => _server;
        set
        {
            Debug.Log("server" + _server);
            _server = value;
        }
    }
    // �A���_�[����Ȃ��ƃG���[�ɂȂ�̂�
    public int Debug_ //�ݔ����x��(�f�o�b�O)�p�̃Q�b�^�[�Z�b�^�[
    {
        get => _debug;
        set
        {
            Debug.Log("debug" + _debug);
            _debug = value;
        }
    }
    public int Sns //�ݔ����x��(�L��)�p�̃Q�b�^�[�Z�b�^�[
    {
        get => _sns;
        set
        {
            Debug.Log("sns" + _sns);
            _sns = value;
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
