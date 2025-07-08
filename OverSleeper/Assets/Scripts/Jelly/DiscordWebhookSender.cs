using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.IO;

public class DiscordWebhookSender : MonoBehaviour
{
    public static DiscordWebhookSender Instance;

    [Header("Webhook設定JSONパス")]
    public string webhookJsonPath = "Config/webhook.json";

    private string webhookUrl;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadWebhookUrl();
    }

    void LoadWebhookUrl()
    {
        string fullPath = Path.Combine(Application.dataPath, webhookJsonPath);
        if (!File.Exists(fullPath))
        {
            Debug.LogWarning("Webhook設定ファイルが見つかりません: " + fullPath);
            return;
        }

        try
        {
            string json = File.ReadAllText(fullPath);
            WebhookConfig config = JsonUtility.FromJson<WebhookConfig>(json);
            webhookUrl = config.webhookUrl;
            Debug.Log("Webhook URLを読み込みました");
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Webhook読み込み中にエラー: " + ex.Message);
        }
    }

    /// <summary>
    /// 任意の名前・アイコンURLでDiscordに送信
    /// </summary>
    public void SendMessageToDiscord(string message, string userName, string avatarUrl)
    {
        if (string.IsNullOrEmpty(webhookUrl))
        {
            Debug.LogError("Webhook URLが設定されていません");
            return;
        }

        StartCoroutine(PostToDiscord(message, userName, avatarUrl));
    }

    private IEnumerator PostToDiscord(string message, string userName, string avatarUrl)
    {
        string jsonPayload = JsonUtility.ToJson(new DiscordMessage
        {
            username = userName,
            avatar_url = avatarUrl,
            content = message
        });

        UnityWebRequest request = new UnityWebRequest(webhookUrl, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonPayload);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Discord送信エラー: " + request.error);
        }
        else
        {
            Debug.Log($"Discord送信成功: {userName}");
        }
    }

    [System.Serializable]
    public class DiscordMessage
    {
        public string username;
        public string avatar_url;
        public string content;
    }

    [System.Serializable]
    public class WebhookConfig
    {
        public string webhookUrl;
    }
}
