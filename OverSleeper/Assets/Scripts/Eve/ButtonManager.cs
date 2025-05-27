using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonManager : MonoBehaviour,
    IPointerEnterHandler,
    IPointerExitHandler,
    IPointerClickHandler
{
    public ButtonController buttonController;     //ボタンの処理を実行するスクリプト

    //カーソルがImageに来た時の処理
    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonController.OnEnterCursor();
    }

    //カーソルがImageから離れた時の処理
    public void OnPointerExit(PointerEventData eventData)
    {
        buttonController.OnExitCursor();
    }

    //カーソルがImageをクリックした時の処理
    public void OnPointerClick(PointerEventData eventData)
    {
        buttonController.OnClickCursor();
    }

}
