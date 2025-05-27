using System;
using UnityEngine;
using System.IO;
using System.Text;
using System.Security.Cryptography;

[System.Serializable]

public class SaveDataValue
{
    public int _money;          //������
    public int _maintain;       //�ێ���p
    public int _year;           //�o�ߔN��
    public int _month;          //�o�ߌ���
    public int _server;         //�T�[�o�[level
    public int _debug;          //�f�o�b�Olevel
    public int _sns;            //SNSlevel
    public int _user;           //���[�U�[��
    public int _famous;         //�l�C�x
    public int _popular;        //�m���x
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
        Debug.Log("�ۑ�����܂���:" + json);
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
            Debug.Log("�t�@�C����������܂���");
        }
    }

}
