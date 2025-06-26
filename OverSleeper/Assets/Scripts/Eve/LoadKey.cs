using UnityEngine;
public class LoadKey : MonoBehaviour,IChildBehavior
{
    FadeInOut fade;  // フェード機能

    // もしデータがないのなら表示する
    [SerializeField] GameObject GrayObj;

    // セーブ機能
   [SerializeField] SaveJSON saveJSON;

    private void Awake()
    {
        // 一度非表示にしてから判断
        GrayObj.SetActive(false);

        // データない場合の処理
        saveJSON = FindObjectOfType<SaveJSON>();
        if(saveJSON==null)
        {
            GrayObj.SetActive(true);
            Debug.LogWarning("SaveJSONオブジェクトがないよ");
            return;
        }
    }

    public void Execute()
    {
        if (saveJSON != null)
        {
            saveJSON.LoadData();
            Debug.Log("LoadData()の読み込み成功");
        }
        else
        {
            Debug.LogWarning("SaveJSONオブジェクトがないよ");
            return;
        }
        fade = FadeInOut.CreateInstance(); // フェードオブジェクトの生成
        fade.LoadScene("Game");
    }
}
