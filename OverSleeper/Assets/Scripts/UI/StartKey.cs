using UnityEngine;

public class StartKey : MonoBehaviour, IChildBehavior
{
    FadeInOut fade;  // フェード機能

    // インターフェース
    // タイトルからゲームスタートのボタン
    public void Execute()
    {
        fade = FadeInOut.CreateInstance(); // フェードオブジェクトの生成
        fade.LoadScene("Game");
    }
}
