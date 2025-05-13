using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [Header("AudioSource�̂����I�u�W�F�N�g���w��")]
    [SerializeField] private GameObject bgmObject; 
    [SerializeField] private GameObject seObject;

    public SoundGroup[] soundGroups; //BGM,SE��Sound�ϐ��̔z��

    private AudioSource bgmSource;
    private AudioSource seSource;

    private void Awake()
    {
        // �V���O���g��
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        // AudioSource�̎擾
        if (bgmObject != null)
        {
            bgmSource = bgmObject.GetComponent<AudioSource>();
            if (bgmSource != null)
                bgmSource.loop = true;
        }

        if (seObject != null)
        {
            seSource = seObject.GetComponent<AudioSource>();
            if (seSource != null)
                seSource.loop = false;
        }

        if (bgmSource == null || seSource == null)
        {
            Debug.LogError("AudioSource ���������ݒ肳��Ă��܂���I");
        }
    }

    public void PlayBGM(string soundName)//PlayBGM���\�b�h��ʂ̃X�N���v�g�Ōďo��
    {
        Sound sound = FindSound("BGM", soundName);
        if (sound != null && bgmSource != null)
        {
            bgmSource.clip = sound.clip;
            bgmSource.volume = sound.volume;
            bgmSource.loop = sound.loop;
            bgmSource.Play();
        }
        else
        {
            Debug.LogWarning("BGM���Đ��ł��܂���: " + soundName);//�x���G���[
        }
    }

    public void StopBGM()//�ʂ̃X�N���v�g�Ōďo��
    {
        if (bgmSource != null)
            bgmSource.Stop();
    }

    public void PlaySE(string soundName)//�ʂ̃X�N���v�g�ŗǂ�?��s�B
    {
        Sound sound = FindSound("SE", soundName);
        if (sound != null && seSource != null)
        {
            seSource.PlayOneShot(sound.clip, sound.volume);
        }
        else
        {
            Debug.LogWarning("SE���Đ��ł��܂���: " + soundName);
        }
    }

    private Sound FindSound(string groupName, string soundName)
    {
        SoundGroup group = Array.Find(soundGroups, g => g.groupname == groupName);
        if (group == null)
        {
            Debug.LogWarning("SoundGroup��������܂���: " + groupName);//�x���G���[
            return null;
        }

        Sound sound = Array.Find(group.sounds, s => s.name == soundName);
        if (sound == null)
        {
            Debug.LogWarning("Sound��������܂���: " + soundName);//�x���G���[
        }

        return sound;
    }
}
