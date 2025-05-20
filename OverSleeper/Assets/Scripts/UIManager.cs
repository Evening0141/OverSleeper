using UnityEngine;

public class UIManager : MonoBehaviour
{
    // 音量設定のやつ
    MasterKey masterVol;
    BGMKey bgmVol;
    SEKey seVol;

    private SelectButton[] selButtons; // UIButtonの配列

    private LocalUIManager localUISc;
    // シーン共通機能の呼び出し
    public void GeneralCall()
    {
        // コンポーネント取得
        Find();
        // 設定データを読み込む
        masterVol.LOAD();
        bgmVol.LOAD();
        seVol.LOAD();
    }
    // ローカルで作成している機能をここで呼び出し
    public void LocalCall_GAME()
    {
        // コンポーネント取得
        localUISc = GameObject.Find("LocalUIManager").GetComponent<LocalUIManager>();
        // 実行
        localUISc.LocalStart();
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
        // ボタンの実装
        if (Input.GetMouseButtonDown(0))
        {
            // 各スクリプトで条件を満たしているものを実行する
            // 条件は各selButtonsの中で判定するものとする
            foreach (var button in selButtons)
            {
                button.ButtonUpdate();
            }
        }
    }
}
