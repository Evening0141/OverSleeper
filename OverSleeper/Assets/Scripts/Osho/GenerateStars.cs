
using UnityEngine;

public static class GenerateStars
{
    // 人気度の星を出力する処理
    public static string Generate(int value)
    {
        // 最大値を制限
        int max = 5;
        value = Mathf.Clamp(value, 0, max); // 0〜5に制限

        string result = "";

        // ★を追加
        for (int i = 0; i < value; i++)
        {
            result += "★";
        }

        // ☆を追加
        for (int i = 0; i < max - value; i++)
        {
            result += "☆";
        }

        return result;
    }
}
