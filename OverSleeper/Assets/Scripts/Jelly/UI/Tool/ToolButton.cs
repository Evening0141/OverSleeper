using UnityEngine;
using UnityEngine.UI;
public class ToolButton : MonoBehaviour
{
    /// <summary>
    /// level,moneyをテキストに反映させる
    /// </summary>
    /// <param name="level">ToolDataのlevel</param>
    /// <param name="money">ToolDataのmoney</param>
    /// <param name="levelText">levelの反映先</param>
    /// <param name="moneyText">moneyの反映先</param>
    protected void SetText(int level, int money, Text levelText, Text moneyText)
    {
        // コンポーネント取得
        Transform child_week = transform.Find("LevelText");
        Transform child_money = transform.Find("moneyText");
        levelText = child_week.GetComponentInChildren<Text>();
        moneyText = child_money.GetComponentInChildren<Text>();
        // 表示
        levelText.text = "Lv." + level.ToString();
        moneyText.text = "費用:" + money.ToString() + "万";
    }
}
