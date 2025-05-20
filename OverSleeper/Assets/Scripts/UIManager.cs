using UnityEngine;

public class UIManager : MonoBehaviour
{
    // ���ʐݒ�̂��
    MasterKey masterVol;
    BGMKey bgmVol;
    SEKey seVol;

    private SelectButton[] selButtons; // UIButton�̔z��

    private LocalUIManager localUISc;
    // �V�[�����ʋ@�\�̌Ăяo��
    public void GeneralCall()
    {
        // �R���|�[�l���g�擾
        Find();
        // �ݒ�f�[�^��ǂݍ���
        masterVol.LOAD();
        bgmVol.LOAD();
        seVol.LOAD();
    }
    // ���[�J���ō쐬���Ă���@�\�������ŌĂяo��
    public void LocalCall_GAME()
    {
        // �R���|�[�l���g�擾
        localUISc = GameObject.Find("LocalUIManager").GetComponent<LocalUIManager>();
        // ���s
        localUISc.LocalStart();
    }

    /// <summary>
    /// �R���|�[�l���g�擾�Ƃ��̃��\�b�h
    /// </summary>
    public void Find()
    {
        // volume�̃X���C�_�[�R���|�[�l���g�擾
        masterVol = GameObject.Find("MasterMusicSlider").GetComponent<MasterKey>();
        bgmVol = GameObject.Find("BGMMusicSlider").GetComponent<BGMKey>();
        seVol = GameObject.Find("SEMusicSlider").GetComponent<SEKey>();

        // �V�[�����̃{�^��������ǂݍ���
        selButtons = FindObjectsOfType<SelectButton>(true);

        // �eSelectButton��Start���Ăяo��
        foreach (var button in selButtons)
        {
            button.ButtonStart();
        }
    }

    // Update is called once per frame
    public void UIUpdate()
    {
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
}
