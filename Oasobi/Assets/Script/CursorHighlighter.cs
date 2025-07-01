using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CursorHighlighter : MonoBehaviour
{
    public Image cursorImage;

    public string targetTag = "MovableObj";

    public Color defaultCol = Color.white;
    public Color highlilghtCol = Color.red;

    private void Update()
    {
        //プレイヤーが動くもしくは視点が動いた際に起動したい
        CheckCursorRoutine();
    }

    private void CheckCursorRoutine()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.CompareTag(targetTag))
            {
                cursorImage.color = highlilghtCol;
                return;
            }
        }
        cursorImage.color = defaultCol;
    }

}
