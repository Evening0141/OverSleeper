using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveKey : MonoBehaviour,IChildBehavior
{
    // インターフェース
    public void Execute()
    {
        Debug.Log("セーブするよ");
    }
}
