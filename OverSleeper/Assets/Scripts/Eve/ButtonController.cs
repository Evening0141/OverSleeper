using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    [SerializeField] ButtonAction buttonAction;             //Inspector上で変更する為の変数

    public Image changeImage;           //色やクリックの反応をさせたいImage
    public GameObject closeImage;       //上のImageをクリックした際、非表示になるImage
    public GameObject openImage;        //上とは逆に表示されるImage
    public Image folderBackGround;      //ButtonActionがFolderの時、後ろに表示される背景

    private Color32 red = new Color32(255, 0, 0, 255);         //赤
    private Color32 white = new Color32(255, 255, 255, 255);   //白
    private Color32 yellow = new Color32(255, 255, 0, 255);    //黄色
    //ButtonManagerのOnPointerEnterからアクセスがあった時作動

    //このスクリプトがコンポーネントされているImageの設定
    //Inspector上で変更する
    public enum ButtonAction
    { 
        Folder,                 //ImageがFolderの時
        CloseButton,            //ImageがCloseButtonの時
    }
    public void OnEnterCursor()
    {
        changeImage.color = red;
    }

    //ButtonManagerのOnPointerExitからアクセスがあった時作動
    public void OnExitCursor()
    {
        changeImage.color = white;
    }

    //ButtonManagerのOnPointerClickからアクセスがあった時作動
    public void OnClickCursor()
    {
        if(buttonAction==ButtonAction.Folder)
        {

        }
        else if(buttonAction==ButtonAction.CloseButton)
        {
            changeImage.color = yellow;
        }
        closeImage.SetActive(false);
        openImage.SetActive(true);
    }
}
