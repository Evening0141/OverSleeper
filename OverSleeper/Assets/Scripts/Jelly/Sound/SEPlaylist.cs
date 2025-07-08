using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "SEPlaylist", menuName = "Audio/SE Playlist")]
public class SEPlaylist : ScriptableObject
{
    [Header("SE�N���b�v�ꗗ�iAudioClip�̂݁j")]
    public List<AudioClip> seClips = new List<AudioClip>();
}