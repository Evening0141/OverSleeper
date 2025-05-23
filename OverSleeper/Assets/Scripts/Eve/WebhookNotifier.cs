using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class WebhookNotifier : MonoBehaviour
{
    // あなたのWebhook URLをここに貼ってください（漏洩注意）
    private string webhookUrl = "https://discord.com/api/webhooks/1331491437048631366/Gv306sjAzk4hP4y73nhUQyS-ALauGF1d3y9FhloIKIX5NfJSx4hIFWwSxM5VzZSaxArZ";

    public void NotifyCheat(string message)
    {
        StartCoroutine(SendWebhook(message));
    }

    IEnumerator SendWebhook(string message)
    {
        // Discord用のJSON形式に整形
        string jsonPayload = "{\"content\":\"🚨 不正検出: " + message + "\"}";

        UnityWebRequest request = new UnityWebRequest(webhookUrl, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonPayload);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("Webhook送信失敗: " + request.error);
        }
        else
        {
            Debug.Log("Webhook送信成功");
        }
    }
}
