using UnityEngine;
using UnityEngine.UI;

public class TextSpacer : TextSpacerBase
{
    // �R���|�[�l���g�擾
    [Header("�����Ԋu��t����e�L�X�g�I�u�W�F�N�g��ݒ�"),SerializeField] Text useText;

    public void TextSpace()
    {
        ApplySpacedText(useText);
    }
}
