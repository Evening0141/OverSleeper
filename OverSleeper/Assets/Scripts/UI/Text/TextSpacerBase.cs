using UnityEngine;
using UnityEngine.UI;

public class TextSpacerBase : MonoBehaviour
{
    // �X�y�[�X���i���L�ݒ�\�j
    [SerializeField] protected int spaceCount = 1;

    // �����ԂɃX�y�[�X��ǉ����鏈��
    protected string AddSpacesBetweenCharacters(string input)
    {
        string space = new string(' ', Mathf.Max(1, spaceCount));
        string result = "";
        for (int i = 0; i < input.Length; i++)
        {
            result += input[i];
            if (i < input.Length - 1)
                result += space;
        }
        return result;
    }

    // Text�̒��g��u�������鏈��
    protected void ApplySpacedText(Text targetText)
    {
        if (targetText == null || string.IsNullOrEmpty(targetText.text))
            return;

        string original = targetText.text;
        targetText.text = AddSpacesBetweenCharacters(original);
    }
}
