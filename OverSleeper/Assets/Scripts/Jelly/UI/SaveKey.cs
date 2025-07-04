using UnityEngine;

public class SaveKey : MonoBehaviour,IChildBehavior
{
    [SerializeField] private SaveJSON saveJSON;
    [SerializeField] private Animator anim; // �Z�[�u�����ۂ�UI�ŕ\�L����

    // �C���^�[�t�F�[�X
    // �Z�[�u�@�\
    public void Execute()
    {
        AnimatorStateInfo state = anim.GetCurrentAnimatorStateInfo(0);

        if (state.IsName("N"))
        {
            Debug.Log("�Z�[�u�����");

            if (saveJSON != null)
            {
                anim.Play("DOWN");
                saveJSON.SaveData();
            }
            else
            {
                Debug.LogError("SaveJSON�����ݒ肾��");
            }
        }
        // �Z�[�u�o���Ȃ��ꍇ��SE�����炵�܂�
        else
        {

        }
    }
}
