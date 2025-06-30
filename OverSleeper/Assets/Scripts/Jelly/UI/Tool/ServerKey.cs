using UnityEngine;
using UnityEngine.UI;

public class ServerKey : MonoBehaviour,IChildBehavior
{
    Text levelText;  // レベルを表示するテキスト
    Text moneyText;  // 費用を表示するテキスト

    private int cost;
    private int level = -1;

    // テキスト反映

    private void Awake()
    {
        Cost();
        // 一応チェック
        if (level == -1) {
            Debug.Log("データを正しく受け取れませんでした。");
            return; }
        // コンポーネント取得
        Transform child_week = transform.Find("LevelText");
        Transform child_money = transform.Find("moneyText");
        levelText = child_week.GetComponentInChildren<Text>();
        moneyText = child_money.GetComponentInChildren<Text>();
        // 表示
        levelText.text = "Lv." + level.ToString();
        moneyText.text = "費用:" + cost.ToString("N0") + "万";
    }

    // コスト計算
    private void Cost()
    {
        level = DataRelay.Dr.Server;
        // サーバーだけ少し安く設定しておく
        cost = Calculation.GetNextLevelCost(level);
    }

    // インターフェース
    // 設備ボタンの中のサーバー機能です
    public void Execute()
    {
        Debug.Log("レベルアップ");

        if (cost<=DataRelay.Dr.Money)
        {
            // レベルアップ
            DataRelay.Dr.Server++;
            DataRelay.Dr.Money -= cost;
            Cost();
            // 表示
            levelText.text = "Lv." + level.ToString();
            moneyText.text = "費用:" + cost.ToString("N0") + "万";
        }
    }
}
