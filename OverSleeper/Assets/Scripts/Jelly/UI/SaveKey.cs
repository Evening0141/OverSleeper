using UnityEngine;

public class SaveKey : MonoBehaviour,IChildBehavior
{
    // インターフェース
    // セーブ機能
    public void Execute()
    {
        Debug.Log("セーブするよ");
    }
}
