using UnityEngine;
using UnityEngine.UI;    // UI関連
using UnityEngine.EventSystems; // マウス操作関連

public class Item : MonoBehaviour,
    IBeginDragHandler, // ドラッグ開始
    IDragHandler,      // ドラッグ中
    IEndDragHandler    // ドラッグ終了 
{
    private Image image;
    private RectTransform rectTransform;
    private bool isDrag = false;
    private CanvasGroup cGroup;
    void Awake()
    {
        image = GetComponent<Image>();
        rectTransform = GetComponent<RectTransform>();
        cGroup = GetComponent<CanvasGroup>();
    }

    void Update()
    {
        if(isDrag)
        {
            if(Input.GetMouseButtonDown(1))
            {
                rectTransform.localEulerAngles +=
                    new Vector3(0, 0, -90);
            }
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        isDrag = true;
        // レイ貫通
        cGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        isDrag = true;
        // ポインタの位置に補正
        rectTransform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDrag = false;
        // レイを防ぐ
        cGroup.blocksRaycasts = true;
    }

}
