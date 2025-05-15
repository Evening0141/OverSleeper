using UnityEngine;

public class EndKey : MonoBehaviour,IChildBehavior
{
    // インターフェース
    public void Execute()
    {
        Application.Quit();

        // エディタでも反応
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
