using UnityEngine;
using System.IO;

[System.Serializable]

public class SaveDataValue
{
    public int _money;          //所持金
    public int _maintain;       //維持費用
    public int _year;           //経過年数
    public int _month;          //経過月数
    public int _server;         //サーバーlevel
    public int _debug;          //デバッグlevel
    public int _sns;            //SNSlevel
    public int _user;           //ユーザー数
    public int _famous;         //人気度
    public int _popular;        //知名度
}

public class SaveJSON : MonoBehaviour
{
    string savePath => Application.persistentDataPath + "/save.sec";            //暗号化するなら使うやつ
    DataRelay relay;                        //DataRelay継承

    private void Awake()
    {
        relay = DataRelay.Dr;               //初期化
    }


    /// <summary>
    /// ファイルに保存
    /// </summary>
    public void SaveData()
    {
        ///<summary>
        ///データ受け取り関数
        ///</summary>
        SaveDataValue data = new SaveDataValue
        {
            _money = relay.Money,
            _maintain = relay.Maintain,
            _year = relay.Year,
            _month = relay.Month,
            _server = relay.Server,
            _debug = relay.Debug_,
            _sns = relay.Sns,
            _user = relay.User,
            _famous = relay.Famous,
            _popular = relay.Popular,
        };


        string json = JsonUtility.ToJson(data);         //文字列に変換
        File.WriteAllText(Application.persistentDataPath + "/save.json", json);         //保存
        Debug.Log("保存されました:" + json);             //保存されたデータの内容
        Debug.Log(Application.persistentDataPath);      //保存したファイルの場所
    }

    /// <summary>
    /// ファイルの読み込み
    /// </summary>
    public void LoadData()
    {
        string path = Application.persistentDataPath + "/save.json";
        //jsonファイルが存在するかの確認
        if (File.Exists(path))
        {
            //存在する場合は復元
            string json = File.ReadAllText(path);
            SaveDataValue data = JsonUtility.FromJson<SaveDataValue>(json);

            //読み込まれたデータをロード
            relay.Money = data._money;
            relay.Maintain = data._maintain;
            relay.Year = data._year;
            relay.Month = data._month;
            relay.Server = data._server;
            relay.Debug_ = data._debug;
            relay.Sns = data._sns;
            relay.User = data._user;
            relay.Famous = data._famous;
            relay.Popular = data._popular;

            Debug.Log("データが読み込まれました：" + json);
        }
        else
        {
            //無ければログを出す
            Debug.Log("ファイルが見つかりません");
        }
    }

}
