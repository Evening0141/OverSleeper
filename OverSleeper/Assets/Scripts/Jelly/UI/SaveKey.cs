using UnityEngine;

public class SaveKey : MonoBehaviour,IChildBehavior
{
    [SerializeField] private SaveJSON saveJSON;
    [SerializeField] private Animator anim; // セーブした際にUIで表記する

    // インターフェース
    // セーブ機能
    public void Execute()
    {
        AnimatorStateInfo state = anim.GetCurrentAnimatorStateInfo(0);

        if (state.IsName("N"))
        {
            Debug.Log("セーブするよ");

            if (saveJSON != null)
            {
                anim.Play("DOWN");
                saveJSON.SaveData();
            }
            else
            {
                Debug.LogError("SaveJSONが未設定だよ");
            }
        }
        // セーブ出来ない場合のSEだけ鳴らします
        else
        {

        }
    }
}
