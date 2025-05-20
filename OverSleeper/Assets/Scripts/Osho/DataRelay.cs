using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataRelay : MonoBehaviour
{
    public int money; //資金用の変数
    public int maintain; //維持費用の変数
    public int year; //年の変数
    public int week; //週の変数
    public int user; //ユーザー数の変数
    public int famous; //人気度の変数
    public int popular;//知名度の変数

    public int server;//サーバーレベル用の変数
    public int debug;//デバッグ用の変数
    public int sns;//広告用の変数

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
        if(this != dr) { Destroy(this.gameObject); return; }//入っているならこのGameobjectを消す。
    }
}
