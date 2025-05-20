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
    public Image image;         //ボタンにしたいImageを入れるためのモノ

    private Color32 pink = new Color32(255, 123, 255, 255);         //ピンク
    private RectTransform buttonRect;           //ボタンのrecttransform

    //カーソルがImageに来た時の処理
    public void OnPointerEnter(PointerEventData eventData)
    {
        image.color = pink;
    }

    //カーソルがImageから離れた時の処理
    public void OnPointerExit(PointerEventData eventData)
    {
        image.color = Color.white;
    }

    //カーソルがImageをクリックした時の処理
    public void OnPointerClick(PointerEventData eventData)
    {
        image.color = Color.yellow;
    }

}
