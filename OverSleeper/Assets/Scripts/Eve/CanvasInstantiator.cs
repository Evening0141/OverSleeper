using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasInstantiator : MonoBehaviour
{
    [SerializeField] private GameObject loadingCanvas;          //Loading���
    void Start()
    {
        string sceneName = "Game";          //Load������Scene��

        GameObject canvasInstance = Instantiate(loadingCanvas);         //Loading��ʂ̐���
        Loading loadingCS = canvasInstance.GetComponent<Loading>();     //Loading��ʃI�u�W�F�N�g�ɂ��Ă���loading.cs���擾
        loadingCS.LoadScene(sceneName);         //Loading.cs��LoadScene()�����s
    }
}
