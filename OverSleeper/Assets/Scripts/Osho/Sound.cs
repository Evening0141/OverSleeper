using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]//���ꂪ�Ȃ���SoundManager��inspector�ɕ\������Ȃ�����
public class Sound
{
    public string name;�@//������^�̕ϐ�name
    public AudioClip clip; //AudioClip
    [HideInInspector]�@public AudioSource audiosr;//�����Ő�������BHide�ɂ��đ����猩��Ȃ��悤��
    public float volume = 1.0f;
    public bool loop = false;
}
