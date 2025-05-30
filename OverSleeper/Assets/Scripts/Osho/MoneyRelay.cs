
using UnityEngine;

public class MoneyRelay
{
    private int money = 0; //資産用の変数
    private float timer = 0; //cooltime用のtimer変数
    public  float MONEY_COOLTIME = 0.5f; //MONEYのクールタイム宣言
    public void MoneyGrow()//別のスクリプトで呼出し
    {
        //時間計算用
        timer += Time.deltaTime; 
        if (timer >= MONEY_COOLTIME)
        {
            Debug.Log("通っている");
            //cooltimeごとに資金を定数分増やす処理
            money = Calculation.GetMoney(money);
            //DateRelayに値を返す。
            timer = 0;
            DataRelay.Dr.Money += money; 
            money = 0;
        }
    }
       
}
