using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "BGMPlaylist", menuName = "Audio/BGM Playlist")]
public class BGMPlaylist : ScriptableObject
{
    [Header("BGM�N���b�v�ꗗ�iAudioClip�̂݁j")]
    public List<AudioClip> bgmClips = new List<AudioClip>();
}