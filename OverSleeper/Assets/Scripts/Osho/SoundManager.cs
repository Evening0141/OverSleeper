using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    public SoundGroup[] soundGroups;

    public void Play(string groupName, string soundName)
    {
        Sound sound = FindSound(groupName, soundName); //Findで検索し、soundに代入
        if (sound != null)//soundが存在しているなら
            sound.audiosr.Play();//起動
    }

    public void Stop(string groupName, string soundName)
    {
        Sound sound = FindSound(groupName, soundName); //Findで検索し、soundに代入
        if (sound != null) //soundが存在しているなら
            sound.audiosr.Stop();//停止
    }
    private Sound FindSound(string groupName, string soundName)
    {
        SoundGroup group = Array.Find(soundGroups, g => g.groupname == groupName); //groupnameをFind
        if (group == null) { Debug.LogWarning("エラー発生"); return null; }//警告エラー表示用

        Sound sound = Array.Find(group.sounds, s => s.name == soundName);//soundnameをFind
        if (sound == null) { Debug.LogWarning("エラー発生"); return null; }//警告エラー表示用

        return sound;
    }
}
