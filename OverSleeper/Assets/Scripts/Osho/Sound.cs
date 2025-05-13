using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]//これがないとSoundManagerのinspectorに表示されないため
public class Sound
{
    public string name;　//文字列型の変数name
    public AudioClip clip; //AudioClip
    [HideInInspector]　public AudioSource audiosr;//内部で生成する。Hideにして他から見れないように
    public bool loop = false;
    [Range(0.0f,1.0f)]public float volume = 1.0f;
}
