using System;
using UnityEngine;
using System.IO;
using System.Text;
using System.Security.Cryptography;

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
    string savePath => Application.persistentDataPath + "/save.sec";
    DataRelay _dataRelay;

    private void Start()
    {
        _dataRelay = GetComponent<DataRelay>();
    }

    public void SaveData()
    {
        SaveDataValue data = new SaveDataValue
        {
            _money = _dataRelay.Money,
            _maintain = _dataRelay.Maintain,
            _year = _dataRelay.Year,
            _month = _dataRelay.Month,
            _server = _dataRelay.Server,
            _debug = _dataRelay.Debug_,
            _sns = _dataRelay.Sns,
            _user = _dataRelay.User,
            _famous = _dataRelay.Famous,
            _popular=_dataRelay.Popular,
        };

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/save.json", json);
        Debug.Log("保存されました:" + json);
    }

    public void LoadData()
    {
        string path = Application.persistentDataPath + "/save.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveDataValue data = JsonUtility.FromJson<SaveDataValue>(json);
        }
        else
        {
            Debug.Log("ファイルが見つかりません");
        }
    }

}
