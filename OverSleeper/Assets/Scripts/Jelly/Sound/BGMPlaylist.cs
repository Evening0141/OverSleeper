using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "BGMPlaylist", menuName = "Audio/BGM Playlist")]
public class BGMPlaylist : ScriptableObject
{
    [Header("BGMクリップ一覧（AudioClipのみ）")]
    public List<AudioClip> bgmClips = new List<AudioClip>();
}