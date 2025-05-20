using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonManager : MonoBehaviour,
    IPointerEnterHandler,
    IPointerExitHandler,
    IPointerClickHandler
{
    public ButtonController buttonController;     //�{�^���̏��������s����X�N���v�g

    //�J�[�\����Image�ɗ������̏���
    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonController.OnEnterCursor();
    }

    //�J�[�\����Image���痣�ꂽ���̏���
    public void OnPointerExit(PointerEventData eventData)
    {
        buttonController.OnExitCursor();
    }

    //�J�[�\����Image���N���b�N�������̏���
    public void OnPointerClick(PointerEventData eventData)
    {
        buttonController.OnClickCursor();
    }

}
