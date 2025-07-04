using UnityEngine;
using UnityEngine.UI;

public static class ColorChange
{
    /// 点滅用の赤色を返す（α値で変化）
    public static Color GetFlashRed(float speed = 2f)
    {
        float alpha = Mathf.PingPong(Time.time * speed, 1f); // 0〜1を往復
        return new Color(1f, 0f, 0f, alpha); // 赤（R=1,G=0,B=0）の透明度を変える
    }

    // 対象のTextの色を赤点滅に更新する
    public static void ChangeCol(Text text, float speed = 2f)
    {
        text.color = GetFlashRed(speed);
    }

}
