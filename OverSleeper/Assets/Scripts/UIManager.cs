using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    // 音量設定のやつ
    VolumeController volController;
    public void UIStart()
    {
        // コンポーネント取得
        Find();
        // 設定データを読み込む
        volController.LOAD();
    }

    /// <summary>
    /// コンポーネント取得とかのメソッド
    /// </summary>
    public void Find()
    {
        // volumeのスライダーコンポーネント取得
        volController = GameObject.Find("MusicSlider").GetComponent<VolumeController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
