using UnityEngine;

public class LocalUIManager : MonoBehaviour
{
    private TextSpacer[] spaceCreate; // 空白文字を追加するスクリプト

    // UnityEditorの処理
    #region DEBUG
    // UIManagerはTitleシーンからの継続させるので
    // ここはデバッグ用に置いてあります
#if UNITY_EDITOR
    private SelectButton[] selButtons; // UIButtonの配列
    GameObject debugBlocker;

    private void Start()
    {
        // 実行時に特定オブジェクトを探す
        debugBlocker = GameObject.Find("UIManager");

        if (debugBlocker != null)
        {
            Debug.Log("処理は実行しません");
            return;  // 処理を止める
        }

        // シーン内の処理を読み込む
        spaceCreate = FindObjectsOfType<TextSpacer>(true);
        // 処理実行
        foreach (var text in spaceCreate)
        {
            text.TextSpace();
        }

        // シーン内のボタン処理を読み込む
        selButtons = FindObjectsOfType<SelectButton>(true);

        // 各SelectButtonのStartを呼び出す
        foreach (var button in selButtons)
        {
            button.ButtonStart();
        }
    }
    private void Update()
    {
        Debug.Log("現在資金" + DataRelay.Dr.money);

        if (debugBlocker != null)
        {
            Debug.Log("処理は実行しません");
            return;  // 処理を止める
        }
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
#endif
    #endregion


    // Main処理はここから
    // UIManagerで呼び出す形で作成
    public void LocalStart()
    {
        // シーン内の処理を読み込む
        spaceCreate = FindObjectsOfType<TextSpacer>(true);
        // 処理実行
        foreach (var text in spaceCreate)
        {
            text.TextSpace();
        }
    }
}
