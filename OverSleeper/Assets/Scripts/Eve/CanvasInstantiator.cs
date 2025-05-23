using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasInstantiator : MonoBehaviour
{
    [SerializeField] private GameObject loadingCanvas;

    // Start is called before the first frame update
    void Start()
    {
        string sceneName = "Game";

        GameObject canvasInstance = Instantiate(loadingCanvas);
        Loading loadingCS = canvasInstance.GetComponent<Loading>();
        loadingCS.LoadScene(sceneName);
    }
}
