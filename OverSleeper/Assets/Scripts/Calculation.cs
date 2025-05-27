using UnityEngine;
using System; // 計算に使う
// 計算のメソッドを入れていきます
public static class Calculation
{
    // コスト計算で使う定数
    private const int COST_BASE = 100;
    private const float COST_MULTI = 1.35f;

    //資金計算で使う変数
    private const int MONEY = 1; //定数
    /// <summary>
    /// 指定レベルにおける次のレベルアップまでのコストを計算して返す
    /// </summary>
    /// <param name="currentLevel">現在のレベル</param>
    /// <returns>次レベルまでの必要コスト</returns>
    public static int GetNextLevelCost(int currentLevel)
    {
        float cost = COST_BASE * (float)Math.Pow(COST_MULTI, currentLevel);
        return Mathf.CeilToInt(cost); // 小数切り上げて整数に変換
    }

    //資金計算
    public static int GetMoney(int  currentmoney)
    {
        return currentmoney + MONEY; //お金を増やす処理に変更　
    }
}
