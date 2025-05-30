using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Loading : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Text loadingText;
    private AsyncOperation async;

    public void LoadScene(string sceneName)
    {
        //scene遷移前段階

        StartCoroutine(Load(sceneName));
    }

    IEnumerator Load(string sceneName)
    {
        //遷移しきるまでsliderのvalueを増やし続ける
        async = SceneManager.LoadSceneAsync(sceneName);
        
        while (!async.isDone)
        {
            //Scene読み込みの進行度合いに応じてSliderのValueを増加させる
            float progressVal = Mathf.Clamp01(async.progress / 0.9f);
            slider.value = progressVal;
            float dot = Mathf.Clamp01(async.progress * 3.0f);           //Loading時に表示されるテキスト
            loadingText.text = "接続中";
            yield return null;
        }
    }
}
