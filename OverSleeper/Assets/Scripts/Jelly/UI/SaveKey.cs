using UnityEngine;

public class SaveKey : MonoBehaviour,IChildBehavior
{
    SaveJSON save;
    // �C���^�[�t�F�[�X
    // �Z�[�u�@�\
    public void Execute()
    {
        Debug.Log("�Z�[�u�����");
        save.SaveData();
    }
}
