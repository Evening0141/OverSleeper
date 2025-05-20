using UnityEngine;
using UnityEngine.UI;

public class ServerKey : MonoBehaviour,IChildBehavior
{
    [SerializeField] ToolData toolData; // データの取得
    Text levelText;  // レベルを表示するテキスト
    Text moneyText;  // 費用を表示するテキスト

    // インターフェース
    // 設備ボタンの中のサーバー機能です
    public void Execute()
    {

    }

    // テキスト反映

    private void Awake()
    {
        // コンポーネント取得
        Transform child_week = transform.Find("LevelText");
        Transform child_money = transform.Find("moneyText");
        levelText = child_week.GetComponentInChildren<Text>();
        moneyText = child_money.GetComponentInChildren<Text>();
        // 表示
       // levelText.text = "Lv." + level.ToString();
       // moneyText.text = "費用:" + money.ToString() + "万";
    }
}
