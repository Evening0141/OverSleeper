using UnityEngine;

public class MoneyRelay
{
    private int money = 0; //資産用の変数
    private int retMoney = 0; // 外部に送る資産
    private float timer = 0; //cooltime用のtimer変数
    public  float MONEY_COOLTIME = 2f; //MONEYのクールタイム宣言

    private const int Server_grow = 3;//サーバー用の定数
    private const int Debug_grow = 30;//デバッグ用の定数
    private const int Sns_grow = 100;//SNS用の定数

    private const int MONEY_MAX = 1000000000; //MONEYの上限

  

    public void MoneyGrow()//別のスクリプトで呼出し
    {
        var dr = DataRelay.Dr;

        //時間計算用
        if (dr.Famous == 0)
        {
            timer += Time.deltaTime;
        }
        else
        {
            timer += Time.deltaTime + 0.15f * (float)dr.Famous * Time.deltaTime;
        }
       
        if (timer >= MONEY_COOLTIME)
        {
            //cooltimeごとに資金を定数分増やす処理
            money = Calculation.GetMoney(money) +
              dr.Debug_* Debug_grow +
              dr.Server * Server_grow +
              dr.Sns * Sns_grow;

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

    ///
    public void MoneyCalc()
    {
        var dr = DataRelay.Dr;

        //cooltimeごとに資金を定数分増やす処理
        money = Calculation.GetMoney(money) +
          dr.Debug_ * Debug_grow +
          dr.Server * Server_grow +
          dr.Sns * Sns_grow;

        // 計算とは別にコピーする
        retMoney = money;

        money = 0;
    }

    // 収入の値
    public int ReturnMoney
    {
        get
        {
            return retMoney;
        }
    }
       
}
