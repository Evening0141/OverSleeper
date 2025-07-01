using UnityEngine;

public class Famous
{
    DataRelay dr = DataRelay.Dr;

    private int famous;

    // 時間計算用
    private const float _TIME = 70.0f;
    private float cntTimer = _TIME;

    // チーター出現時の計算用
    private float cntTimerCheatON=_TIME;

    public void FamousCount()
    {
        // 値を代入する。
        // DataRelay側でFamousは上限と下限が決められてるからそれを元にここで調整する
        if (famous != dr.Famous)
        {
            famous = dr.Famous;
        }
        if (!dr.IsCheat)
        {
            cntTimer -= Time.deltaTime;
            if (cntTimer <= 0)
            {
                // 時間再セット
                cntTimer = _TIME;
                famous++;
                dr.Famous = famous;
            }
        }
        // 出現されっぱなしだと人気度を下げる
        else
        {
            cntTimerCheatON -= Time.deltaTime;
            if (cntTimerCheatON <= 0)
            {
                // 時間再セット
                cntTimerCheatON = _TIME;
                famous--;
                dr.Famous = famous;
            }
        }
    }
}
