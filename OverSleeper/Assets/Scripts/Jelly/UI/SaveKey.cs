using UnityEngine;

public class SaveKey : MonoBehaviour,IChildBehavior
{
    SaveJSON save;
    // インターフェース
    // セーブ機能
    public void Execute()
    {
        Debug.Log("セーブするよ");
        save.SaveData();
    }
}
