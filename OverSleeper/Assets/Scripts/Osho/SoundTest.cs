using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundTest : MonoBehaviour
{
    void Start()
    {
        SoundManager.Instance.PlayBGM("�^�C�g��BGM");//�e�X�g
    }

    // Update is called once per frame
    void Update()
    {
        //SoundManager.instance.Play("SE", "Jump");
    }
}
