using UnityEngine;
using UnityEngine.UI;
public class LocalUIManager : MonoBehaviour
{
    private TextSpacer[] spaceCreate; // �󔒕�����ǉ�����X�N���v�g
    #region UI
    [Header("�����\���̃e�L�X�g"), SerializeField] Text moneyText;
    [Header("�ێ���\���̃e�L�X�g"), SerializeField] Text maintainText;
    [Header("�N�\���̃e�L�X�g"), SerializeField] Text yearText;
    [Header("���\���̃e�L�X�g"), SerializeField] Text monthText;
    [Header("�l�C�x�\���̃e�L�X�g"), SerializeField] Text famousText;
    [Header("�m���x�\���̃e�L�X�g"), SerializeField] Text popularText;
    [Header("���[�U�[���\���̃e�L�X�g"), SerializeField] Text userText;
    #endregion  
    // UnityEditor�̏���
    #region DEBUG
    // UIManager��Title�V�[������̌p��������̂�
    // �����̓f�o�b�O�p�ɒu���Ă���܂�
#if UNITY_EDITOR
    private SelectButton[] selButtons; // UIButton�̔z��
    GameObject debugBlocker;

    private void Start()
    {
        // ���s���ɓ���I�u�W�F�N�g��T��
        debugBlocker = GameObject.Find("UIManager");

        if (debugBlocker != null)
        {
            Debug.Log("�����͎��s���܂���");
            return;  // �������~�߂�
        }

        // �V�[�����̏�����ǂݍ���
        spaceCreate = FindObjectsOfType<TextSpacer>(true);
        // �������s
        foreach (var text in spaceCreate)
        {
            text.TextSpace();
        }

        // �V�[�����̃{�^��������ǂݍ���
        selButtons = FindObjectsOfType<SelectButton>(true);

        // �eSelectButton��Start���Ăяo��
        foreach (var button in selButtons)
        {
            button.ButtonStart();
        }
    }
    private void Update()
    {
        Debug.Log("���ݎ���" + DataRelay.Dr.Money);

        if (debugBlocker != null)
        {
            Debug.Log("�����͎��s���܂���");
            return;  // �������~�߂�
        }
        // �{�^���̎���
        if (Input.GetMouseButtonDown(0))
        {

            // �e�X�N���v�g�ŏ����𖞂����Ă�����̂����s����
            // �����͊eselButtons�̒��Ŕ��肷����̂Ƃ���
            foreach (var button in selButtons)
            {
                button.ButtonUpdate();
            }
        }
        UIUpdate();
    }
#endif
    #endregion


    // Main�����͂�������
    // UIManager�ŌĂяo���`�ō쐬
    public void LocalStart()
    {
        // �󔒕����̃X�N���v�g
        #region
        //// �V�[�����̏�����ǂݍ���
        //spaceCreate = FindObjectsOfType<TextSpacer>(true);
        //// �������s
        //foreach (var text in spaceCreate)
        //{
        //    text.TextSpace();
        //}
        #endregion
        // UI�X�V
        UIUpdate();
    }



    // �X�V
    private void UIUpdate()
    {
        // �����̔��f
        moneyText.text = DataRelay.Dr.Money.ToString();
        maintainText.text = DataRelay.Dr.Maintain.ToString();
        yearText.text = DataRelay.Dr.Year.ToString();
        monthText.text = DataRelay.Dr.Month.ToString();
        famousText.text = DataRelay.Dr.Famous.ToString();
        popularText.text = DataRelay.Dr.Popular.ToString();
        userText.text = DataRelay.Dr.User.ToString();
    }
}
