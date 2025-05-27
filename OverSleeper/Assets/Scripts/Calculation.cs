using UnityEngine;
using System; // 計算に使う
// 計算のメソッドを入れていきます
public static class Calculation
{
    // コスト計算で使う定数
    private const int COST_BASE = 100;
    private const float COST_MULTI = 1.35f;
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
}
