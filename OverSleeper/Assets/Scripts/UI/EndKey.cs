using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndKey : MonoBehaviour,IChildBehavior
{
    // �C���^�[�t�F�[�X
    public void Execute()
    {
        Application.Quit();

        // �G�f�B�^�ł�����
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
