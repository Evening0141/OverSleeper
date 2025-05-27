using UnityEngine;
using UnityEngine.UI;

public class ContentKey : DevButton, IChildBehavior
{
    [SerializeField] DevData devData; // �f�[�^�̎擾
    Text weekText;   // �J�����Ԃ�\������e�L�X�g
    Text moneyText;  // ��p��\������e�L�X�g
    // �C���^�[�t�F�[�X
    // �J���{�^���̒��̋@�\�ł�
    public void Execute()
    {

    }

    // �e�L�X�g���f

    private void Awake()
    {
        SetText(devData.week, devData.money, weekText, moneyText);
    }
}
