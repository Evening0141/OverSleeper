using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    // ���ʐݒ�̂��
    MasterKey masterVol;
    BGMKey bgmVol;
    SEKey seVol;
    public void UIStart()
    {
        // �R���|�[�l���g�擾
        Find();
        // �ݒ�f�[�^��ǂݍ���
        masterVol.LOAD();
        bgmVol.LOAD();
        seVol.LOAD();
    }

    /// <summary>
    /// �R���|�[�l���g�擾�Ƃ��̃��\�b�h
    /// </summary>
    public void Find()
    {
        // volume�̃X���C�_�[�R���|�[�l���g�擾
        masterVol = GameObject.Find("MasterMusicSlider").GetComponent<MasterKey>();
        bgmVol = GameObject.Find("BGMMusicSlider").GetComponent<BGMKey>();
        seVol = GameObject.Find("SEMusicSlider").GetComponent<SEKey>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
