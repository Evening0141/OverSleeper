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
        //scene�J�ڑO�i�K

        StartCoroutine(Load(sceneName));
    }

    IEnumerator Load(string sceneName)
    {
        //�J�ڂ�����܂�slider��value�𑝂₵������
        async = SceneManager.LoadSceneAsync(sceneName);
        
        while (!async.isDone)
        {
            float progressVal = Mathf.Clamp01(async.progress / 0.9f);
            slider.value = progressVal;
            float dot = Mathf.Clamp01(async.progress * 3.0f);           //Loading���ɕ\�������e�L�X�g��.�̐�
            loadingText.text = "�ڑ���" /*+ ".".Length * dot*/;
            yield return null;
        }
    }
}
