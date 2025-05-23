using UnityEngine;
using UnityEngine.EventSystems;

public class GridCell : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    private SpriteRenderer sr;
    private Color defCol;              // �����F
    private Color selCol = Color.yellow; // �I��F
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        defCol = sr.color; // ����
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        sr.color = selCol;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        sr.color = defCol;
    }

    void Update()
    {
        
    }
}
