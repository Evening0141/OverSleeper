using UnityEngine;
using UnityEngine.UI;    // UI関連
using UnityEngine.EventSystems; // マウス操作関連

// マスのプログラム
public class Cell : MonoBehaviour,
    IPointerEnterHandler,   // マウスカーソルを合わせた時
    IPointerExitHandler,    // マウスカーソルをはなした時
    IPointerClickHandler    // マウスクリックした時
{
    public int x, y;                     // 自身の座標
    public bool filled = false;          // マスに設置されているか

    private Image imgCmp;                // 画像のコンポーネント
    private Color defCol = Color.white;  // 基本色
    private Color selCol = Color.yellow; // 選択された時
    private Color fillCol = Color.gray;  // 設置済みの時の色

    // 仮
    public GameObject itemPre;  // アイテムを仮で持たせる

    private void Awake()
    {
        imgCmp = GetComponent<Image>();
    }
    public void SetColor(Color setCol)
    {
        if (imgCmp != null) { imgCmp.color = setCol; }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        SetColor(selCol);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        SetColor(filled == true? fillCol:defCol);
    }
    public  void OnPointerClick(PointerEventData eventData)
    {
        if(!filled)
        {
            filled = true;
            SetColor(fillCol);

            // 仮でアイテムをマスの場所に置く
            RectTransform canvasRect = transform.root.GetComponent<RectTransform>();
            Instantiate(itemPre, transform.position, Quaternion.identity, canvasRect);
#if false
            // セルの座標
            RectTransform cellRect = GetComponent<RectTransform>();
            // セルのワールド座標をスクリーン座標に変換
            Vector2 screenPos = 
                RectTransformUtility.WorldToScreenPoint(null, cellRect.position);
            // キャンバスの座標
            RectTransform canvasRect = transform.root.GetComponent<RectTransform>();
            // スクリーン座標からキャンバスのローカル座標に変換
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvasRect, screenPos, null, out Vector2 localPoint);
            // アイテム生成
            GameObject item = Instantiate(itemPre, canvasRect);
            RectTransform itemRect = item.GetComponent<RectTransform>();

            itemRect.anchoredPosition = localPoint;
            itemRect.localScale = Vector3.one;
#endif
        }
    }

}
