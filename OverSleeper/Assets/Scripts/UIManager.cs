using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    // ���ʐݒ�̂��
    VolumeController volController;
    public void UIStart()
    {
        // �R���|�[�l���g�擾
        Find();
        // �ݒ�f�[�^��ǂݍ���
        volController.LOAD();
    }

    /// <summary>
    /// �R���|�[�l���g�擾�Ƃ��̃��\�b�h
    /// </summary>
    public void Find()
    {
        // volume�̃X���C�_�[�R���|�[�l���g�擾
        volController = GameObject.Find("MusicSlider").GetComponent<VolumeController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
