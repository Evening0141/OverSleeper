using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BanEffect : MonoBehaviour
{
    [SerializeField] private Material puchunMaterial;
    [SerializeField] private float duration = 0.5f;

    private float progress = 0f;
    private bool isPlaying = false;

    public void Start()
    {
        if (!isPlaying)
            StartCoroutine(PlayPuchun());
    }

    private System.Collections.IEnumerator PlayPuchun()
    {
        isPlaying = true;
        progress = 0f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            progress = Mathf.Clamp01(elapsed / duration);
            puchunMaterial.SetFloat("_Progress", progress);
            yield return null;
        }

        puchunMaterial.SetFloat("_Progress", 1f);
        isPlaying = false;
    }
}
