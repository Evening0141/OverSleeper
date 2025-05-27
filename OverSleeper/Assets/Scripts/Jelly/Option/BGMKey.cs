using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMKey : VolumeController
{
    // �L�[��������ύX
    protected override string VolumeKey => "BGMVolume";

    [SerializeField] AudioSource bgmAudio;

    protected override void ApplyVolume(float volume)
    {
        if (bgmAudio != null)
        {
            bgmAudio.volume = volume;
        }
    }
}
