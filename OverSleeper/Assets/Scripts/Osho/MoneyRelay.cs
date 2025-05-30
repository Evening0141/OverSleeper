
using UnityEngine;

public class MoneyRelay
{
    private int money = 0; //資産用の変数
    private float timer = 0; //cooltime用のtimer変数
    public  float MONEY_COOLTIME = 5f; //MONEYのクールタイム宣言

    private const int Debug_grow = 100;//デバッグ用の定数
    private const int Server_grow = 10;//サーバー用の定数
    private const int Sns_grow = 1000;//SNS用の定数


    public void MoneyGrow()//別のスクリプトで呼出し
    {
        //時間計算用
        timer += Time.deltaTime; 
        if (timer >= MONEY_COOLTIME)
        {
            Debug.Log("通っている");
            //cooltimeごとに資金を定数分増やす処理
            money = Calculation.GetMoney(money) + DataRelay.Dr.Debug_* Debug_grow + DataRelay.Dr.Server* Server_grow +DataRelay.Dr.Sns* Sns_grow;
            //DateRelayに値を返す。
            timer = 0;
            DataRelay.Dr.Money += money; 
            money = 0;
        }
    }
       
}
