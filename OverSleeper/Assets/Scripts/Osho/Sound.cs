
using UnityEngine;


[System.Serializable]//これがないとSoundManagerのinspectorに表示されないため
public class Sound
{
    public string name;　//文字列型の変数name
    public AudioClip clip; //AudioClip 
    [HideInInspector]　public AudioSource audiosr;//内部で生成する。Hideにして他から見れないように
    public float volume = 1.0f; //音量設定
    public bool loop = false; //Loop処理
}
