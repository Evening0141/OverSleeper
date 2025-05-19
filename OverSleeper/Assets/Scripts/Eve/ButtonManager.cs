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
    public Image image;         //�{�^���ɂ�����Image�����邽�߂̃��m

    private Color32 pink = new Color32(255, 123, 255, 255);         //�s���N
    private RectTransform buttonRect;           //�{�^����recttransform

    //�J�[�\����Image�ɗ������̏���
    public void OnPointerEnter(PointerEventData eventData)
    {
        image.color = pink;
    }

    //�J�[�\����Image���痣�ꂽ���̏���
    public void OnPointerExit(PointerEventData eventData)
    {
        image.color = Color.white;
    }

    //�J�[�\����Image���N���b�N�������̏���
    public void OnPointerClick(PointerEventData eventData)
    {
        image.color = Color.yellow;
    }

}
