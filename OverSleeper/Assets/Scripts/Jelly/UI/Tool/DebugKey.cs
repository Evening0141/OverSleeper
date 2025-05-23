using UnityEngine;
using UnityEngine.UI;

public class DebugKey : MonoBehaviour, IChildBehavior
{
    Text levelText;  // レベルを表示するテキスト
    Text moneyText;  // 費用を表示するテキスト

    private float cost;

    private int level = -1;

    // テキスト反映

    private void Awake()
    {
        level = DataRelay.Dr.Debug_;
        // 一応チェック
        if (level == -1)
        {
            Debug.Log("データを正しく受け取れませんでした。");
            return;
        }
        Cost();
        // コンポーネント取得
        Transform child_week = transform.Find("LevelText");
        Transform child_money = transform.Find("moneyText");
        levelText = child_week.GetComponentInChildren<Text>();
        moneyText = child_money.GetComponentInChildren<Text>();
        // 表示
        levelText.text = "Lv." + level.ToString();
        moneyText.text = "費用:" + cost.ToString() + "万";
    }

    // コスト計算
    private void Cost()
    {
        cost = Calculation.GetNextLevelCost(level);
    }

    // インターフェース
    // 設備ボタンの中のサーバー機能です
    public void Execute()
    {

    }

}
