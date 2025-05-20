using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalUIManager : MonoBehaviour
{
    private TextSpacer[] spaceCreate; // 空白文字を追加するスクリプト

    // UnityEditorの処理
    #region DEBUG
    // UIManagerはTitleシーンからの継続させるので
    // ここはデバッグ用に置いてあります
#if UNITY_EDITOR
    private void Start()
    {
        // 実行時に特定オブジェクトを探す
        GameObject debugBlocker = GameObject.Find("UIManager");

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
    }
    private void Update()
    {
        
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
