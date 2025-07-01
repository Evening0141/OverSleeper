using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadHandler : MonoBehaviour
{
    ////シーンのロードが完了したかの判別用
    //private bool hasLoaded = false;

    //private void Awake()
    //{
    //    DontDestroyOnLoad(gameObject);                  //シーンが切り替わっても消えないオブジェクト
    //    SceneManager.sceneLoaded += OnSceneLoaded;
    //}

    //private void OnDestroy()
    //{
    //    SceneManager.sceneLoaded -= OnSceneLoaded;
    //}

    //private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    //{
    //    //シーンの切り替わり直後で、一度も実行されていないなら処理される
    //    if (scene.name == "Game" && !hasLoaded)
    //    {
    //        hasLoaded = true;

    //        SaveJSON saveJSON = FindObjectOfType<SaveJSON>();
    //        if (saveJSON != null)
    //        {
    //            saveJSON.LoadData();
    //            Debug.Log("LoadData()の読み込み成功");
    //        }
    //        else
    //        {
    //            Debug.LogWarning("SaveJSONオブジェクトがないよ");
    //        }


    //    }
    //}
}
