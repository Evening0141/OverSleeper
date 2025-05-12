using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonManager : MonoBehaviour,
    IPointerEnterHandler,
    IPointerExitHandler,
    IPointerClickHandler
{
    public Image image;

    private Color32 pink = new Color32(255, 123, 255, 255);
    private RectTransform buttonRect;

    public void OnPointerEnter(PointerEventData eventData)
    {
        image.color = pink;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        image.color = Color.white;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        image.color = Color.yellow;
    }

}
