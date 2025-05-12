using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    // 音量設定のやつ
    MasterKey masterVol;
    BGMKey bgmVol;
    SEKey seVol;
    public void UIStart()
    {
        // コンポーネント取得
        Find();
        // 設定データを読み込む
        masterVol.LOAD();
        bgmVol.LOAD();
        seVol.LOAD();
    }

    /// <summary>
    /// コンポーネント取得とかのメソッド
    /// </summary>
    public void Find()
    {
        // volumeのスライダーコンポーネント取得
        masterVol = GameObject.Find("MasterMusicSlider").GetComponent<MasterKey>();
        bgmVol = GameObject.Find("BGMMusicSlider").GetComponent<BGMKey>();
        seVol = GameObject.Find("SEMusicSlider").GetComponent<SEKey>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
