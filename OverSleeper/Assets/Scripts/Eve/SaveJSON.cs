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
    string savePath => Application.persistentDataPath + "/save.sec";            //�Í�������Ȃ�g�����
    DataRelay relay;                        //DataRelay�p��

    private void Awake()
    {
        relay = DataRelay.Dr;               //������
    }


    /// <summary>
    /// �t�@�C���ɕۑ�
    /// </summary>
    public void SaveData()
    {
        ///<summary>
        ///�f�[�^�󂯎��֐�
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


        string json = JsonUtility.ToJson(data);         //������ɕϊ�
        File.WriteAllText(Application.persistentDataPath + "/save.json", json);         //�ۑ�
        Debug.Log("�ۑ�����܂���:" + json);
        Debug.Log(Application.persistentDataPath);
    }

    /// <summary>
    /// �t�@�C���̓ǂݍ���
    /// </summary>
    public void LoadData()
    {
        string path = Application.persistentDataPath + "/save.json";
        //json�t�@�C�������݂��邩�̊m�F
        if (File.Exists(path))
        {
            //���݂���ꍇ�͕���
            string json = File.ReadAllText(path);
            SaveDataValue data = JsonUtility.FromJson<SaveDataValue>(json);
        }
        else
        {
            //������΃��O���o��
            Debug.Log("�t�@�C����������܂���");
        }
    }

}
