using UnityEngine;
using UnityEngine.EventSystems;

public class GridCell : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    private SpriteRenderer sr;
    private Color defCol;              // 初期色
    private Color selCol = Color.yellow; // 選択色
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        defCol = sr.color; // 初期
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
       // sr.color = selCol;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
       // sr.color = defCol;
    }
}
