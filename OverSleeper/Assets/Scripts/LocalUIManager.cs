using UnityEngine;

public class LocalUIManager : MonoBehaviour
{
    private TextSpacer[] spaceCreate; // �󔒕�����ǉ�����X�N���v�g

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
        Debug.Log("���ݎ���" + DataRelay.Dr.money);

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
    }
#endif
    #endregion


    // Main�����͂�������
    // UIManager�ŌĂяo���`�ō쐬
    public void LocalStart()
    {
        // �V�[�����̏�����ǂݍ���
        spaceCreate = FindObjectsOfType<TextSpacer>(true);
        // �������s
        foreach (var text in spaceCreate)
        {
            text.TextSpace();
        }
    }
}
