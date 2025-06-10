
using UnityEngine;
using System.Numerics;

public class MoneyRelay
{
    private int money = 0; //資産用の変数
    private float timer = 0; //cooltime用のtimer変数
    public  float MONEY_COOLTIME = 0.5f; //MONEYのクールタイム宣言

    private const int Debug_grow = 100;//デバッグ用の定数
    private const int Server_grow = 10;//サーバー用の定数
    private const int Sns_grow = 1000;//SNS用の定数
    private const int MONEY_MAX = 1000000000; //MONEYの上限


    public void MoneyGrow()//別のスクリプトで呼出し
    {
        //時間計算用
        timer += Time.deltaTime; 
        if (timer >= MONEY_COOLTIME)
        {
            Debug.Log("通っている");
            //cooltimeごとに資金を定数分増やす処理
            money = Calculation.GetMoney(money) + DataRelay.Dr.Debug_* Debug_grow + DataRelay.Dr.Server* Server_grow +DataRelay.Dr.Sns* Sns_grow;

            // 現在の資金を取得
            int currentMoney = DataRelay.Dr.Money;
            // 10桁の上限 MONEY_MAXを超えないようにチェック
            if (currentMoney <= MONEY_MAX - money)
            {
                DataRelay.Dr.Money += money;
            }
            else
            {
                DataRelay.Dr.Money = MONEY_MAX;//超えた場合MONEY_MAXに固定
            }


            timer = 0;
            money = 0;
        }
    }
       
}
