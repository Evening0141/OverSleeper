using UnityEngine;
using UnityEngine.UI;

public class ChangePanelBase : MonoBehaviour
{
    [Header("切り替え後に非表示にするImage"), SerializeField] Image beforeImg;
    [Header("切り替え後に表示にするImage"), SerializeField] Image afterImg;

    // カラー変数
    const float beforeColAlpha = 0.0f;
    const float afterColAlpha = 0.01f;

    public void ChangePanel()
    {
        // マスクのα値を利用してパネルの切り替えを実装
        beforeImg.color = new Color(1, 1, 1, beforeColAlpha);
        afterImg.color = new Color(1, 1, 1, afterColAlpha);
    }
   

}
