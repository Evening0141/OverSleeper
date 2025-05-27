using System; // ←これが必要！
using UnityEngine;
using System.IO;
using System.Text;
using System.Security.Cryptography;

[System.Serializable]
public class PlayerDataTest
{
    public string PlayerName;
    public int score;
    public bool isAlive;
}

public class JSONTest : MonoBehaviour
{
    string savePath => Application.persistentDataPath + "/save.sec";

    public void Start()
    {
        //SaveData();
        Debug.Log("保存先: " + savePath);

        LoadData();
    }

    void SaveData()
    {
        PlayerDataTest data = new PlayerDataTest
        {
            PlayerName = "Alice",
            score = 12345,
            isAlive = true
        };

        string json = JsonUtility.ToJson(data);
        string base64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(json));
        string hash = GetSHA256(base64);

        string saveString = base64 + ":" + hash;
        File.WriteAllText(savePath, saveString);

        Debug.Log("データ保存完了");
        Debug.Log("保存先: " + savePath);
    }

    void LoadData()
    {
        if (!File.Exists(savePath))
        {
            Debug.Log("保存ファイルが見つかりません");
            return;
        }

        string[] parts = File.ReadAllText(savePath).Split(':');
        if (parts.Length != 2)
        {
            Debug.Log("データ形式が不正です");
            // Webhook送信（外部通知）
            GameObject.FindObjectOfType<WebhookNotifier>()?.NotifyCheat("不正を検知");
            return;
        }

        string base64 = parts[0];
        string savedHash = parts[1];

        if (GetSHA256(base64) != savedHash)
        {
            Debug.Log("改ざんが検出されました！");

            // Webhook送信（外部通知）
            GameObject.FindObjectOfType<WebhookNotifier>()?.NotifyCheat("セーブデータが書き換えられました");
            return;
        }

        string json = Encoding.UTF8.GetString(Convert.FromBase64String(base64));
        PlayerDataTest data = JsonUtility.FromJson<PlayerDataTest>(json);

        Debug.Log("読み込み成功: " + data.PlayerName + ", スコア: " + data.score);
    }

    string GetSHA256(string input)
    {
        using (SHA256 sha = SHA256.Create())
        {
            byte[] bytes = Encoding.UTF8.GetBytes(input);
            byte[] hash = sha.ComputeHash(bytes);
            return BitConverter.ToString(hash).Replace("-", "");
        }
    }
}
