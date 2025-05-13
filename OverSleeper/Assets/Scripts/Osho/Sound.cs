using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]//‚±‚ê‚ª‚È‚¢‚ÆSoundManager‚Ìinspector‚É•\¦‚³‚ê‚È‚¢‚½‚ß
public class Sound
{
    public string name;
    public AudioClip clip;
    [HideInInspector]@public AudioSource audiosr;//“à•”‚Å¶¬‚·‚éBHide‚É‚µ‚Ä‘¼‚©‚çŒ©‚ê‚È‚¢‚æ‚¤‚É
}
