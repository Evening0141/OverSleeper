using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasInstantiator : MonoBehaviour
{
    [SerializeField] private GameObject loadingCanvas;
    [SerializeField] private Loading loadingCS;

    // Start is called before the first frame update
    void Start()
    {
        string sceneName = "OtamesiScene";
        Instantiate(loadingCanvas);
        loadingCS.LoadScene(sceneName);
    }
}
