using UnityEngine;
using UnityEngine.UI;
public class EventKey :DevButton,IChildBehavior
{
    [SerializeField] DevData devData; // データの取得
    Text weekText;   // 開発期間を表示するテキスト
    Text moneyText;  // 費用を表示するテキスト
    // インターフェース
    // 開発ボタンの中のイベント開発機能です
    public void Execute()
    {
    
    }

    // テキスト反映

    private void Awake()
    {
        SetText(devData.week,devData.money,weekText,moneyText);
    }
}
