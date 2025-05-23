using UnityEngine;
using UnityEngine.UI;

public class TextSpacerBase : MonoBehaviour
{
    // スペース数（共有設定可能）
    [SerializeField] protected int spaceCount = 1;

    // 文字間にスペースを追加する処理
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

    // Textの中身を置き換える処理
    protected void ApplySpacedText(Text targetText)
    {
        if (targetText == null || string.IsNullOrEmpty(targetText.text))
            return;

        string original = targetText.text;
        targetText.text = AddSpacesBetweenCharacters(original);
    }
}
