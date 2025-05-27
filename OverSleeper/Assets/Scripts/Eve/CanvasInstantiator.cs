using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasInstantiator : MonoBehaviour
{
    [SerializeField] private GameObject loadingCanvas;          //Loading画面
    void Start()
    {
        string sceneName = "Game";          //LoadしたいScene名

        GameObject canvasInstance = Instantiate(loadingCanvas);         //Loading画面の生成
        Loading loadingCS = canvasInstance.GetComponent<Loading>();     //Loading画面オブジェクトについているloading.csを取得
        loadingCS.LoadScene(sceneName);         //Loading.csのLoadScene()を実行
    }
}
