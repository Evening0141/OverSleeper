using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]//���ꂪ�Ȃ���SoundManager��inspector�ɕ\������Ȃ�����
public class Sound
{
    public string name;�@//������^�̕ϐ�name
    public AudioClip clip; //AudioClip
    [HideInInspector]�@public AudioSource audiosr;//�����Ő�������BHide�ɂ��đ����猩��Ȃ��悤��
    public bool loop = false;
    [Range(0.0f,1.0f)]public float volume = 1.0f;
}
