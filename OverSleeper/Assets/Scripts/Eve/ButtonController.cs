using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    [SerializeField] ButtonAction buttonAction;             //Inspector��ŕύX����ׂ̕ϐ�

    public Image changeImage;           //�F��N���b�N�̔�������������Image
    public GameObject closeImage;       //���Image���N���b�N�����ہA��\���ɂȂ�Image
    public GameObject openImage;        //��Ƃ͋t�ɕ\�������Image
    public Image folderBackGround;      //ButtonAction��Folder�̎��A���ɕ\�������w�i

    private Color32 red = new Color32(255, 0, 0, 255);         //��
    private Color32 white = new Color32(255, 255, 255, 255);   //��
    private Color32 yellow = new Color32(255, 255, 0, 255);    //���F
    //ButtonManager��OnPointerEnter����A�N�Z�X�����������쓮

    //���̃X�N���v�g���R���|�[�l���g����Ă���Image�̐ݒ�
    //Inspector��ŕύX����
    public enum ButtonAction
    { 
        Folder,                 //Image��Folder�̎�
        CloseButton,            //Image��CloseButton�̎�
    }
    public void OnEnterCursor()
    {
        changeImage.color = red;
    }

    //ButtonManager��OnPointerExit����A�N�Z�X�����������쓮
    public void OnExitCursor()
    {
        changeImage.color = white;
    }

    //ButtonManager��OnPointerClick����A�N�Z�X�����������쓮
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
