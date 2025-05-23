using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SEKey : VolumeController
{
    // キー名だけを変更
    protected override string VolumeKey => "SEVolume";
    [SerializeField] AudioSource seAudio;

    protected override void ApplyVolume(float volume)
    {
        if (seAudio != null)
        {
            seAudio.volume = volume;
        }
    }
}
