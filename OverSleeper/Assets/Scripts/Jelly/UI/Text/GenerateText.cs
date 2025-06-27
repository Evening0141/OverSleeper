using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GenerateText : MonoBehaviour
{
    [Header("プレハブ・参照設定")]
    public GameObject charPrefab;         // 1文字表示用のUIプレハブ（Text付き）
    public Transform parentCanvas;        // 親となるCanvas（RectTransform）
    public TextData textData;             // ヒントなどの文字列データ（ScriptableObject）

    [Header("文字表示間隔設定")]
    public float charSpacing = 50f;       // 各文字の間隔（px単位、固定幅）
    public float charOffsetY = 0f;        // 表示Y座標（縦位置の調整）
    public float charDelay = 0.1f;        // 各文字の出現間隔（タイピング風の速度）
    public float spawnInterval = 3f;      // 1つの文章表示が完了してから、次の文章を出すまでの待機時間

    [Header("色変更設定")]
    public Color highlightColor = Color.red;   // 色変更用カラー
    public char[] colorMarkers = new char[] { '&', '%', '$' }; // 特定の記号（次の1文字を色付け）

    private bool isSpawning = false;      // 現在文章を生成中かどうかのフラグ（重複生成を防ぐ）

    /// <summary>
    /// 外部から呼び出してヒント表示を開始する
    /// </summary>
    public void TIPSText()
    {
        if (!isSpawning)
        {
            StartCoroutine(GenerateHintCoroutine());
        }
    }

    /// <summary>
    /// コルーチンで1文字ずつTIPSを生成して表示する（タイピング風演出＋特定文字色）
    /// </summary>
    IEnumerator GenerateHintCoroutine()
    {
        isSpawning = true;

        // テキストデータが存在するか確認
        if (textData == null || textData.hintMessages.Length == 0)
        {
            Debug.LogWarning("TextDataが設定されていないか、メッセージが空です。");
            yield break;
        }

        // ランダムに1つのメッセージを選択
        string message = textData.hintMessages[Random.Range(0, textData.hintMessages.Length)];

        float xOffset = 800f; // 開始位置（画面右側）
        bool nextCharIsColored = false;

        // 1文字ずつ処理
        for (int i = 0; i < message.Length; i++)
        {
            char c = message[i];

            // 色変更用の記号かどうかをチェック
            if (IsColorMarker(c))
            {
                nextCharIsColored = true;
                continue; // 記号は表示しない
            }

            // プレハブを生成
            GameObject charObj = Instantiate(charPrefab, parentCanvas);
            Text text = charObj.GetComponentInChildren<Text>();
            string charStr = c.ToString();
            text.text = charStr;

            // 色変更フラグが立っていれば色を変更
            if (nextCharIsColored)
            {
                text.color = highlightColor;
                nextCharIsColored = false;
            }

            // 座標を設定
            RectTransform rt = charObj.GetComponent<RectTransform>();
            rt.anchoredPosition = new Vector2(xOffset, charOffsetY);
            xOffset += charSpacing;

            // 次の文字まで待機（タイピング風演出）
            yield return new WaitForSeconds(charDelay);
        }

        // 全文表示後、次のメッセージまで待機
        yield return new WaitForSeconds(spawnInterval);
        isSpawning = false;
    }

    /// <summary>
    /// 指定した文字が色変更マーカーかどうかを判定
    /// </summary>
    private bool IsColorMarker(char c)
    {
        foreach (char marker in colorMarkers)
        {
            if (c == marker) return true;
        }
        return false;
    }
}
