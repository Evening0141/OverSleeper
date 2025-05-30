
using UnityEngine;

public class DateRelay
{
    private float timer;
    private const int MONTH_MAX = 12;　//1年（12か月）の定数宣言
    public float DATE_COOLTIME = 5.0f; //Dateのクールタイム

   
    public void DateGrow()
    {
        timer += Time.deltaTime;
        if(timer >= DATE_COOLTIME)
        {
            Debug.Log("Date通っている");
            //cooltimeごとに月を1増やす。
            DataRelay.Dr.Month++;
            //12月に到達したら月を1に戻して年を1増やす。
            if (DataRelay.Dr.Month > MONTH_MAX)　
            {
                DataRelay.Dr.Month = 1;
                DataRelay.Dr.Year++;
            }
            timer = 0;
        }
    }
}
