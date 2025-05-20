using UnityEngine;
using UnityEngine.UI;

public class TextSpacer : TextSpacerBase
{
    // コンポーネント取得
    [Header("文字間隔を付けるテキストオブジェクトを設定"),SerializeField] Text useText;

    public void TextSpace()
    {
        ApplySpacedText(useText);
    }
}
