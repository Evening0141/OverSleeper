using UnityEngine;

public class StartKey : MonoBehaviour, IChildBehavior
{
    FadeInOut fade;  // �t�F�[�h�@�\

    // �C���^�[�t�F�[�X
    // �^�C�g������Q�[���X�^�[�g�̃{�^��
    public void Execute()
    {
        fade = FadeInOut.CreateInstance(); // �t�F�[�h�I�u�W�F�N�g�̐���
        fade.LoadScene("Game");
    }
}
