using UnityEngine;
using UnityEngine.UI;

public class DevButton : MonoBehaviour
{
    /// <summary>
    /// week,moneyをテキストに反映させる
    /// </summary>
    /// <param name="week">devDataのweek</param>
    /// <param name="money">devDataのmoney</param>
    /// <param name="weekText">weekの反映先</param>
    /// <param name="moneyText">moneyの反映先</param>
    protected void SetText(int week,int money,Text weekText,Text moneyText)
    {
        // コンポーネント取得
        Transform child_week = transform.Find("weekText");
        Transform child_money = transform.Find("moneyText");
        weekText = child_week.GetComponentInChildren<Text>();
        moneyText = child_money.GetComponentInChildren<Text>();
        // 表示
        weekText.text = "期間:" + week.ToString() + "週";
        moneyText.text = "費用:" + money.ToString() + "万";
    }
}
