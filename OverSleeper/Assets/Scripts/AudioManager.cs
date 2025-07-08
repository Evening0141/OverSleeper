using UnityEngine;

public class AudioManager
{
    private BGMPlaylist bgmPlayL;       // 再生するBGM
    private SEPlaylist sePlayL;       // 再生するBGM
    private GameObject _bgm;            // 再生するオブジェクト
    private GameObject _se;             // 再生するオブジェクト
    private AudioSource audioSource_bgm;// コンポーネント取得用
    private AudioSource audioSource_se; // コンポーネント取得用

    string SoundName_BGM;　// DataRelayからの受け取り
    string SoundName_SE;   // DataRelayからの受け取り

    private DataRelay dr;               // 再生するサウンド名を取得

    private int BGMListNum = 0;

    // コンストラクタ
    public AudioManager()
    {
        // 設定
        dr = DataRelay.Dr;

        // リソースから呼び出し
        bgmPlayL = Resources.Load<BGMPlaylist>("BGMPlaylist");
        sePlayL = Resources.Load<SEPlaylist>("SEPlaylist");
        GameObject prefab = Resources.Load<GameObject>("SOUND");

        // オブジェクトを生成そしてコンポーネント取得
        _bgm = Object.Instantiate(prefab);
        _se = Object.Instantiate(prefab);
        audioSource_bgm = _bgm.GetComponent<AudioSource>();
        audioSource_se = _se.GetComponent<AudioSource>();
    }

    /// <summary>
    /// 音量をここで変更するのでアップデート必須かも
    /// </summary>
    public void VolumeSet()
    {
        audioSource_bgm.volume=PlayerPrefs.GetFloat("BGMVolume", 0.5f);
        audioSource_se.volume=PlayerPrefs.GetFloat("SEVolume", 0.5f);
    }

    public void Play_BGM()
    {
        SoundName_BGM = dr.Data_BGM.ToString();

        if (!audioSource_bgm.isPlaying)
        {
            for (int i = 0; i < bgmPlayL.bgmClips.Count; i++)
            {
                // 一致したものを再生する
                if (SoundName_BGM == bgmPlayL.bgmClips[i].name)
                {
                    audioSource_bgm.clip = bgmPlayL.bgmClips[i];
                    audioSource_bgm.Play();
                    dr.Data_BGM = DataRelay.BGM_Name.None;

                    // 選曲番号をここでマックスになったら０
                    if (i == bgmPlayL.bgmClips.Count - 1)
                    {
                        BGMListNum = 0;
                    }
                    else
                    {
                        BGMListNum = i;
                    }
                    Debug.Log(i);
                }
                else if (SoundName_BGM == "None")
                {
                    audioSource_bgm.clip = bgmPlayL.bgmClips[BGMListNum + 1];
                    audioSource_bgm.Play();
                }
            }
        }
    }

    public void Play_SE()
    {
        SoundName_SE = dr.Data_SE.ToString();

        if (SoundName_SE == "None") { return; }
        for (int i = 0; i < sePlayL.seClips.Count; i++)
        {
            // 一致したものを再生する
            if (SoundName_SE == sePlayL.seClips[i].name)
            {
                audioSource_se.clip = sePlayL.seClips[i];
                audioSource_se.Play();
                dr.Data_SE = DataRelay.SE_Name.None;
            }
            Debug.Log("SE"+SoundName_SE);
        }
    }
}
