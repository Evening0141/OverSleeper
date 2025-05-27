
using UnityEngine;

public class MoneyRelay
{
    public int money = 0; //資産用の変数
    public float timer = 0; //cooltime用のtimer変数
    private const float cool_time = 5.0f; //cooltime
    public void MoneyGrow()//別のスクリプトで呼出し
    {
        timer += Time.deltaTime; //時間計算用
        if (timer >= cool_time)
        {
            Debug.Log("通っている");
            money = Calculation.GetMoney(money);
            DataRelay.Dr.Money += money;
            timer = 0;
            money = 0;
        }
    }
       
}
