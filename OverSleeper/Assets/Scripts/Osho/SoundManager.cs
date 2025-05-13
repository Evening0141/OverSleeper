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
        Sound sound = FindSound(groupName, soundName); //Find�Ō������Asound�ɑ��
        if (sound != null)//sound�����݂��Ă���Ȃ�
            sound.audiosr.Play();//�N��
    }

    public void Stop(string groupName, string soundName)
    {
        Sound sound = FindSound(groupName, soundName); //Find�Ō������Asound�ɑ��
        if (sound != null) //sound�����݂��Ă���Ȃ�
            sound.audiosr.Stop();//��~
    }
    private Sound FindSound(string groupName, string soundName)
    {
        SoundGroup group = Array.Find(soundGroups, g => g.groupname == groupName); //groupname��Find
        if (group == null) { Debug.LogWarning("�G���[����"); return null; }//�x���G���[�\���p

        Sound sound = Array.Find(group.sounds, s => s.name == soundName);//soundname��Find
        if (sound == null) { Debug.LogWarning("�G���[����"); return null; }//�x���G���[�\���p

        return sound;
    }
}
