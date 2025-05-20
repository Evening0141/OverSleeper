using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [Header("AudioSourceのついたオブジェクトを指定")]
    [SerializeField] private GameObject bgmObj; 
    [SerializeField] private GameObject seObj;

    public SoundGroup[] soundGroups; //BGM,SEのSound変数の配列

    private AudioSource bgmSource; //BGMのAudioSource
    private AudioSource seSource;//SEのAudioSource

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

        //DontDestroyOnLoad(gameObject); //シーンを跨ぐ際に消えないようにする処理。要らんかも

        // AudioSourceの取得
        if (bgmObj != null) 
        {
            bgmSource = bgmObj.GetComponent<AudioSource>();
            if (bgmSource != null)
                bgmSource.loop = true;
        }

        if (seObj != null)
        {
            seSource = seObj.GetComponent<AudioSource>();
            if (seSource != null)
                seSource.loop = false;
        }

        if (bgmSource == null || seSource == null)
        {
            Debug.LogError("AudioSource が正しく設定されていません！");//一応の警告エラー
        }
    }

    public void PlayBGM(string soundName)//別のスクリプトで呼び出す。書式→SoundManager.instance.PlayBGM(" ");
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

    public void PlaySE(string soundName)//別のスクリプトで呼び出す。書式→SoundManager.instance.PlayBGM(" ");
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
        SoundGroup group = Array.Find(soundGroups, g => g.groupname == groupName);//soundGroupからGroupnameと一致する値を探す。
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
