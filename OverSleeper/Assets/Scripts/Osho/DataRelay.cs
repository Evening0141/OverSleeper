using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataRelay : MonoBehaviour
{
    private int money = 1000; //資金用の変数
    private int maintain = 0; //維持費用の変数
    private int year = 0; //年の変数
    private int week = 0; //週の変数
    private int user = 0; //ユーザー数の変数
    private int famous = 0; //人気度の変数
    private int popular = 0;//知名度の変数

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
    public int Money //Money(資金)用のゲッターセッター
    {
        get => money;
        set
        {
            Debug.Log("money"+ money);
            money = value;
        }
    }
    public int Maintain　//Maintain(維持費)用のゲッターセッター
    {
        get => maintain;
        set
        {
            Debug.Log("maintain" + maintain);
            maintain = value;
        }
    }
    public int Year　//Year(年)用のゲッターセッター
    {
        get => year;
        set
        {
            Debug.Log("year" + year);
            year = value;
        }
    }
    public int Week //Weak(週)用のゲッターセッター
    {
        get => week;
        set
        {
            Debug.Log("week" +week);
            week = value;
        }
    }
    public int User //User(ユーザー)用のゲッターセッター
    {
        get => user;
        set
        {
            Debug.Log("user" +user);
            user = value;
        }
    }
    public int Famous //Famous(人気度)用のゲッターセッター
    {
        get => famous;
        set
        {
            Debug.Log("famous" +famous);
            famous = value;
        }
    }
    public int Popular //Popular(知名度)用のゲッターセッター
    {
        get => popular;
        set
        {
            Debug.Log("popular" +popular);
            popular = value;
        }
    }
    private void Awake()
    {
        if (dr != null && dr != this)
        {
            Destroy(this.gameObject); // 重複を防止
        }
        else
        {
            dr = this;
            DontDestroyOnLoad(this.gameObject); // シーンをまたいでも残す
        }
    }

}
