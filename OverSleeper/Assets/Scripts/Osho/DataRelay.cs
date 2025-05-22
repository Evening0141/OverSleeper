using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataRelay : MonoBehaviour
{
    public int money = 0; //資金用の変数
    public int maintain = 0; //維持費用の変数
    public int year = 0; //年の変数
    public int week = 0; //週の変数
    public int user = 0; //ユーザー数の変数
    public int famous = 0; //人気度の変数
    public int popular = 0;//知名度の変数

    #region Level
    public int server = 1;//サーバーレベル用の変数
    public int debug = 1;//デバッグ用の変数
    public int sns = 1;//広告用の変数
    #endregion

    //シングルトン用変数
    private static DataRelay dr;
    //シングルトン生成
    public static DataRelay Dr
    {
        get
        {
            if(dr == null)
            {
                dr = (DataRelay)FindObjectOfType(typeof(DataRelay));
            }
            return dr;
        }
    }
    private void Awake()
    {
        if(dr == null)
        {
            dr = this;
        }
        if(this != dr) { Destroy(this.gameObject); return; }//入っているならこのGameobjectを消す。
    }
}
