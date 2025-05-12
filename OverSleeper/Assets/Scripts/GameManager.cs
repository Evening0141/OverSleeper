using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Field
    [Header("UIManagerスクリプト"),SerializeField] UIManager manager_UI;

    private static GameManager instance; // Singletonインスタンス

    FadeInOut fade;  // フェード機能

    // シーンごとに機能を変更していく
    private enum SceneType
    {
        Title,    // タイトルシーン
        Game,     // ゲームシーン
        GameOver, // ゲームオーバーシーン
        Unknown   // 想定外のシーン名用
    }

    // タイトルからはじめる
    SceneType type = SceneType.Title;
    #endregion

    void Awake()
    {
        // Singletonパターン: 他のUIManagerが既に存在する場合、現在のインスタンスを削除
        if (instance != null)
        {
            Destroy(gameObject); // 重複したインスタンスを削除
        }
        else
        {
            instance = this; // 初回のみインスタンスを設定
            DontDestroyOnLoad(gameObject); // このオブジェクトをシーンが変更されても破棄しない

            // シーン読み込み時のイベント登録
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
    }

    void OnDestroy()
    {
        // イベント解除（念のため）
        if (instance == this)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }
    // シーン読み込み後に呼ばれる
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // コンポーネント取得とかのスタートメソッドを任意で呼び出す
        Ready();
        
        switch (scene.name)　// enumのセットをここで
        {
            case "Title":
                type = SceneType.Title;
                break;
            case "Game":
                type = SceneType.Game; 
                break;
            case "GameOver":
                type = SceneType.GameOver;
                break;
            default:
                Debug.LogWarning("未定義のシーン: " + scene.name);
                break;
        }
    }
    private void Update()
    {
        switch (type)
        {
            // タイトルシーンの処理
            case SceneType.Title:
                HandleTitleScene();
                break;
            // ゲームシーンの処理
            case SceneType.Game:
                HandleGameScene();
                break;
            // ゲームオーバーシーンの処理
            case SceneType.GameOver:
                HandleGameOverScene();
                break;
            default:
                break;
        }

    }

    // 各シーンごとの処理
    private void HandleTitleScene()
    {
        Debug.Log("タイトルシーンに遷移しました");
       // fade = FadeInOut.CreateInstance(); // フェードオブジェクトの生成
       // fade.LoadScene("Game");
        // 
    }

    private void HandleGameScene()
    {
        Debug.Log("ゲームシーンに遷移しました");
        // 
    }

    private void HandleGameOverScene()
    {
        Debug.Log("ゲームオーバーシーンに遷移しました");
        // 
    }


    // 任意でスタートメソッドをよびだしたいので
    private void Ready()
    {
        manager_UI.UIStart();
    }
}
