using UnityEngine;

public class BANKEY : MonoBehaviour, IChildBehavior
{
    // �A�j���[�V�����ݒ�
    [SerializeField] Animator anim;
    // �Ώۂ�Rect
    [SerializeField] RectTransform rct;
    //�I�u�W�F�N�g���W�����Ɏ��̃A�j���[�V�������\���`�F�b�N
    const float posY = 550.0f;

    // BAN�̎g�p��
    private bool CanBan = true;

    // �N�[���^�C���v�Z�p
    private const float _TIME = 5.0f;
    private float _timer = _TIME;


    // �C���^�[�t�F�[�X
    // BAN�@�\
    public void Execute()
    {
        if (rct.anchoredPosition.y >= posY&&CanBan)
        {
            CanBan = false;

            Debug.Log("BAN�N���I");
            anim.Play("CLOSE");

            // �`�[�g�̃`�F�b�N
            if (DataRelay.Dr.IsCheat)
            {
                Debug.Log("�������I");
                // �`�[�g�̉���
                DataRelay.Dr.IsCheat = false;
            }
            else // ���Ȃ��ꍇ�̏���
            {
                Debug.Log("��BAN����");
                DataRelay.Dr.Money = DataRelay.Dr.Money / 2; // ���z��
                DataRelay.Dr.Famous -= 1; // �l�C�x������Ƃ�
            }
        }
        else
        {
            Debug.Log("�f�o�b�O�`�F�b�N");
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

        if(_timer<=0)
        {
            // �ăZ�b�g
            _timer = _TIME;
            CanBan = true;

            anim.Play("OPEN");
        }
    }
}
