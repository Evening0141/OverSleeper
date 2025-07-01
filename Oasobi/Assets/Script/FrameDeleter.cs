using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FrameDeleter : MonoBehaviour
{
    [SerializeField] public Image[] deleteImage;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < deleteImage.Length; i++)
        {
            deleteImage[i].color = new Color32(0, 0, 0, 0);
        }
    }
}
