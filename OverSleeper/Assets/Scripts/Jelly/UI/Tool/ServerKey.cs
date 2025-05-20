using UnityEngine;
using UnityEngine.UI;

public class ServerKey : MonoBehaviour,IChildBehavior
{
    Text levelText;  // レベルを表示するテキスト
    Text moneyText;  // 費用を表示するテキスト

    private float cost;

    // インターフェース
    // 設備ボタンの中のサーバー機能です
    public void Execute()
    {

    }

    // テキスト反映

    private void Awake()
    {
        Cost();
        // コンポーネント取得
        Transform child_week = transform.Find("LevelText");
        Transform child_money = transform.Find("moneyText");
        levelText = child_week.GetComponentInChildren<Text>();
        moneyText = child_money.GetComponentInChildren<Text>();
        // 表示
        levelText.text = "Lv." + DataRelay.Dr.server.ToString();
        moneyText.text = "費用:" + cost.ToString() + "万";
    }

    // コスト計算
    private void Cost()
    {
        cost = (float)DataRelay.Dr.server * 1.3f * 100.0f;
    }
}
