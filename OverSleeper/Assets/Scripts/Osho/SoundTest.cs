using UnityEngine;

public class SoundTest : MonoBehaviour
{
    void Start()
    {
        SoundManager.Instance.PlayBGM("タイトルBGM");//テスト
    }

    // Update is called once per frame
    void Update()
    {
        //SoundManager.instance.Play("SE", "Jump");
    }
}
