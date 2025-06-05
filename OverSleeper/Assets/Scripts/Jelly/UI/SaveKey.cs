using UnityEngine;

public class SaveKey : MonoBehaviour,IChildBehavior
{
    [SerializeField] private SaveJSON saveJSON;
    // インターフェース
    // セーブ機能
    public void Execute()
    {
        Debug.Log("セーブするよ");

        if (saveJSON != null)
        {
            saveJSON.SaveData();
        }
        else
        {
            Debug.LogError("SaveJSONが未設定だよ");
        }
    }
}
