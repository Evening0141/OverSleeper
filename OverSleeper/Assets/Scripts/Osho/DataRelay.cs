using UnityEngine;

public class DataRelay : MonoBehaviour
{
    /* ���C���̐��l�\�� */
    #region NumValue
    private int money = 100; //�����p�̕ϐ�
    private int maintain = 0; //�ێ���p�̕ϐ�
    private int year = 0;     //�N�̕ϐ�
    private int month = 0;    //���̕ϐ�

    public int Money //Money(����)�p�̃Q�b�^�[�Z�b�^�[
    {
        get => money;
        set
        {
            Debug.Log("money" + money);
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
    public int Month //Month(��)�p�̃Q�b�^�[�Z�b�^�[
    {
        get => month;
        set
        {
            Debug.Log("month" + month);
            month = value;
        }
    }
    #endregion
    /* ���x���֘A */
    #region Level
    private int _server = 1;  //�T�[�o�[���x���p�̕ϐ�
    private int _debug = 1;   //�f�o�b�O�p�̕ϐ�
    private int _sns = 1;     //�L���p�̕ϐ�

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
    #endregion
    /* ���UI�̕\�� */
    #region 
    /* 
     �����̒l�͂����܂ł�������ł̕\���ƂȂ邪�ۑ����₷���悤��int�Ŏ�舵�� 
     ���ۂɕ\���Ɏg��UIMng���Ő��l�����ԃe�L�X�g�ɕύX���Ă��炤
    */
    // �������A���A�㏸���̎O����
    private int user = 1;            // ���[�U�[���̕ϐ�
    private const int user_MAX = 2;  // ���[�U�[���̏��
    private const int user_MIN = 0;  // ���[�U�[���̉���

    // ��0���灙5�܂ł�6����
    private int famous = 0;             //�l�C�x�̕ϐ�
    private const int famous_MAX = 5;   //�l�C�x�̏��
    private const int famous_MIN = 0;   //�l�C�x�̉���

    // F,E,D,C,B,A,S��7����
    private int popular = 0;            //�m���x�̕ϐ�
    private const int popular_MAX = 0;  //�m���x�̏��
    private const int popular_MIN = 0;  //�m���x�̉���

    public int User //User(���[�U�[)�p�̃Q�b�^�[�Z�b�^�[
    {
        get => user;
        set
        {
            // �͈͐���
            int clampedValue = Mathf.Clamp(value, user_MIN, user_MAX);

            if (clampedValue != value)
            {
                Debug.LogWarning($"User�l���͈͊O�̂��ߕ␳����܂���: ����={value}, �␳��={clampedValue}");
            }

            Debug.Log("user: " + clampedValue);
            user = clampedValue;
        }
    }
    public int Famous //Famous(�l�C�x)�p�̃Q�b�^�[�Z�b�^�[
    {
        get => famous;
        set
        {
            // �͈͐���
            int clampedValue = Mathf.Clamp(value, famous_MIN, famous_MAX);

            if (clampedValue != value)
            {
                Debug.LogWarning($"Famous�l���͈͊O�̂��ߕ␳����܂���: ����={value}, �␳��={clampedValue}");
            }

            Debug.Log("user: " + clampedValue);
            famous = clampedValue;
        }
    }
    public int Popular //Popular(�m���x)�p�̃Q�b�^�[�Z�b�^�[
    {
        get => popular;
        set
        {
            // �͈͐���
            int clampedValue = Mathf.Clamp(value, popular_MIN, popular_MAX);

            if (clampedValue != value)
            {
                Debug.LogWarning($"Popular�l���͈͊O�̂��ߕ␳����܂���: ����={value}, �␳��={clampedValue}");
            }

            Debug.Log("user: " + clampedValue);
            popular = clampedValue;
        }
    }
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
