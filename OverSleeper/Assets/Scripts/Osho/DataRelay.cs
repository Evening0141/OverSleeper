using UnityEngine;

public class DataRelay : MonoBehaviour
{
    /* メインの数値表示 */
    #region NumValue
    private int money = 100; //資金用の変数
    private int maintain = 0; //維持費用の変数
    private int year = 0;     //年の変数
    private int month = 0;    //月の変数

    public int Money //Money(資金)用のゲッターセッター
    {
        get => money;
        set
        {
            Debug.Log("money" + money);
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
    public int Month //Month(月)用のゲッターセッター
    {
        get => month;
        set
        {
            Debug.Log("month" + month);
            month = value;
        }
    }
    #endregion
    /* レベル関連 */
    #region Level
    private int _server = 1;  //サーバーレベル用の変数
    private int _debug = 1;   //デバッグ用の変数
    private int _sns = 1;     //広告用の変数

    public int Server //設備レベル(サーバー)用のゲッターセッター
    {
        get => _server;
        set
        {
            Debug.Log("server" + _server);
            _server = value;
        }
    }
    // アンダー入れないとエラーになるので
    public int Debug_ //設備レベル(デバッグ)用のゲッターセッター
    {
        get => _debug;
        set
        {
            Debug.Log("debug" + _debug);
            _debug = value;
        }
    }
    public int Sns //設備レベル(広告)用のゲッターセッター
    {
        get => _sns;
        set
        {
            Debug.Log("sns" + _sns);
            _sns = value;
        }
    }
    #endregion
    /* 情報UIの表示 */
    #region 
    /* 
     ここの値はあくまでも文字列での表示となるが保存しやすいようにintで取り扱い 
     実際に表示に使うUIMng側で数値から状態テキストに変更してもらう
    */
    // 減少中、一定、上昇中の三項目
    private int user = 1;            // ユーザー数の変数
    private const int user_MAX = 2;  // ユーザー数の上限
    private const int user_MIN = 0;  // ユーザー数の下限

    // ☆0から☆5までの6項目
    private int famous = 0;             //人気度の変数
    private const int famous_MAX = 5;   //人気度の上限
    private const int famous_MIN = 0;   //人気度の下限

    // F,E,D,C,B,A,Sの7項目
    private int popular = 0;            //知名度の変数
    private const int popular_MAX = 0;  //知名度の上限
    private const int popular_MIN = 0;  //知名度の下限

    public int User //User(ユーザー)用のゲッターセッター
    {
        get => user;
        set
        {
            // 範囲制限
            int clampedValue = Mathf.Clamp(value, user_MIN, user_MAX);

            if (clampedValue != value)
            {
                Debug.LogWarning($"User値が範囲外のため補正されました: 入力={value}, 補正後={clampedValue}");
            }

            Debug.Log("user: " + clampedValue);
            user = clampedValue;
        }
    }
    public int Famous //Famous(人気度)用のゲッターセッター
    {
        get => famous;
        set
        {
            // 範囲制限
            int clampedValue = Mathf.Clamp(value, famous_MIN, famous_MAX);

            if (clampedValue != value)
            {
                Debug.LogWarning($"Famous値が範囲外のため補正されました: 入力={value}, 補正後={clampedValue}");
            }

            Debug.Log("user: " + clampedValue);
            famous = clampedValue;
        }
    }
    public int Popular //Popular(知名度)用のゲッターセッター
    {
        get => popular;
        set
        {
            // 範囲制限
            int clampedValue = Mathf.Clamp(value, popular_MIN, popular_MAX);

            if (clampedValue != value)
            {
                Debug.LogWarning($"Popular値が範囲外のため補正されました: 入力={value}, 補正後={clampedValue}");
            }

            Debug.Log("user: " + clampedValue);
            popular = clampedValue;
        }
    }
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
