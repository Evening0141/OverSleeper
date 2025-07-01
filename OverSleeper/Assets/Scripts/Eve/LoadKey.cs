using UnityEngine;
public class LoadKey : MonoBehaviour,IChildBehavior
{
    FadeInOut fade;  // �t�F�[�h�@�\

    // �����f�[�^���Ȃ��̂Ȃ�\������
    [SerializeField] GameObject GrayObj;

    // �Z�[�u�@�\
   [SerializeField] SaveJSON saveJSON;

    private void Awake()
    {
        // ��x��\���ɂ��Ă��画�f
        GrayObj.SetActive(false);

        // �f�[�^�Ȃ��ꍇ�̏���
        saveJSON = FindObjectOfType<SaveJSON>();
        if(saveJSON==null)
        {
            GrayObj.SetActive(true);
            Debug.LogWarning("SaveJSON�I�u�W�F�N�g���Ȃ���");
            return;
        }
    }

    public void Execute()
    {
        if (saveJSON != null)
        {
            saveJSON.LoadData();
            Debug.Log("LoadData()�̓ǂݍ��ݐ���");
        }
        else
        {
            Debug.LogWarning("SaveJSON�I�u�W�F�N�g���Ȃ���");
            return;
        }
        fade = FadeInOut.CreateInstance(); // �t�F�[�h�I�u�W�F�N�g�̐���
        fade.LoadScene("Game");
    }
}
