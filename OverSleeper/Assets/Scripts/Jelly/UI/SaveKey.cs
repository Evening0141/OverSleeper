using UnityEngine;

public class SaveKey : MonoBehaviour,IChildBehavior
{
    [SerializeField] private SaveJSON saveJSON;
    // �C���^�[�t�F�[�X
    // �Z�[�u�@�\
    public void Execute()
    {
        Debug.Log("�Z�[�u�����");

        if (saveJSON != null)
        {
            saveJSON.SaveData();
        }
        else
        {
            Debug.LogError("SaveJSON�����ݒ肾��");
        }
    }
}
