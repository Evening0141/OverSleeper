using UnityEngine;
using UnityEngine.UI;
public class BANKEY : MonoBehaviour, IChildBehavior
{
    // �A�j���[�V�����ݒ�
    [SerializeField] Animator anim;
    // �Ώۂ�Rect
    [SerializeField] RectTransform rct;
    // �e�L�X�g�\��
    [SerializeField] Text text;
    //�I�u�W�F�N�g���W�����Ɏ��̃A�j���[�V�������\���`�F�b�N
    const float posY = 550.0f;

    // BAN�̎g�p��
    private bool CanBan = true;

    // �N�[���^�C���v�Z�p
    private const float _TIME = 5.0f;
    private float _timer = _TIME;

    // ���̃X�N���v�g�ł݈̂����^�C�~���O
    private bool localCheat = false;


    // �C���^�[�t�F�[�X
    // BAN�@�\
    public void Execute()
    {
        if (rct.anchoredPosition.y >= posY&&CanBan)
        {
            CanBan = false;
            localCheat = false; // ����
            text.text = "�C����"; // �����e�L�X�g
            Debug.Log("BAN�N���I");
            anim.Play("CLOSE");

            // �`�[�g�̃`�F�b�N
            if (DataRelay.Dr.IsCheat)
            {
                localCheat = true;
            }
            else // ���Ȃ��ꍇ�̏���
            {
                localCheat = false;
            }
            FuncChaet();
        }
        else
        {
            Debug.Log("�f�o�b�O�`�F�b�Nanim");
        }
    }

    // BAN�̒l��n���ēx�g�p�ł��邩�`�F�b�N
    public bool CanBAN
    {
        get { return CanBan; }
    }

    // �N�[���^�C���v�Z
    public void CoolTime()
    {
        _timer -= Time.deltaTime;

        if (_timer<=0)
        {
            // �ăZ�b�g
            _timer = _TIME;
            CanBan = true;

            anim.Play("OPEN");
        }
    }

    // BAN�������̏���
    private void FuncChaet()
    {
        if (localCheat)
        {
            text.color = Color.red;
            text.text = "B.A.N";
            // �`�[�g�̉���
            DataRelay.Dr.IsCheat = false;
        }
        else
        {
            text.color = Color.white;
            text.text = "���s";
            DataRelay.Dr.Money = DataRelay.Dr.Money / 2; // ���z��
            DataRelay.Dr.Famous -= 1; // �l�C�x������Ƃ�
        }
    }
}
