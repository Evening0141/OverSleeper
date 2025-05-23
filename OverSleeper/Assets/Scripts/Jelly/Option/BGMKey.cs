using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMKey : VolumeController
{
    // キー名だけを変更
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
