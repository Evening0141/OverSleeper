using UnityEngine;

public class UIManager : MonoBehaviour
{
    // 音量設定のやつ
    MasterKey masterVol;
    BGMKey bgmVol;
    SEKey seVol;

    private SelectButton[] selButtons; // UIButtonの配列
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

        // シーン内のボタン処理を読み込む
        selButtons = FindObjectsOfType<SelectButton>(true);

        // 各SelectButtonのStartを呼び出す
        foreach (var button in selButtons)
        {
            button.ButtonStart();
        }
    }

    // Update is called once per frame
    public void UIUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // ボタンの実装
            foreach (var button in selButtons)
            {
                button.ButtonUpdate();
            }
        }
    }
}
