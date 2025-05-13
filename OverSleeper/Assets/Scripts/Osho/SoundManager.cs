using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [Header("AudioSourceのついたオブジェクトを指定")]
    [SerializeField] private GameObject bgmObject; 
    [SerializeField] private GameObject seObject;

    public SoundGroup[] soundGroups; //BGM,SEのSound変数の配列

    private AudioSource bgmSource;
    private AudioSource seSource;

    private void Awake()
    {
        // シングルトン
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

        // AudioSourceの取得
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
            Debug.LogError("AudioSource が正しく設定されていません！");
        }
    }

    public void PlayBGM(string soundName)//PlayBGMメソッドを別のスクリプトで呼出し
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
            Debug.LogWarning("BGMが再生できません: " + soundName);//警告エラー
        }
    }

    public void StopBGM()//別のスクリプトで呼出し
    {
        if (bgmSource != null)
            bgmSource.Stop();
    }

    public void PlaySE(string soundName)//別のスクリプトで良い?台s。
    {
        Sound sound = FindSound("SE", soundName);
        if (sound != null && seSource != null)
        {
            seSource.PlayOneShot(sound.clip, sound.volume);
        }
        else
        {
            Debug.LogWarning("SEが再生できません: " + soundName);
        }
    }

    private Sound FindSound(string groupName, string soundName)
    {
        SoundGroup group = Array.Find(soundGroups, g => g.groupname == groupName);
        if (group == null)
        {
            Debug.LogWarning("SoundGroupが見つかりません: " + groupName);//警告エラー
            return null;
        }

        Sound sound = Array.Find(group.sounds, s => s.name == soundName);
        if (sound == null)
        {
            Debug.LogWarning("Soundが見つかりません: " + soundName);//警告エラー
        }

        return sound;
    }
}
