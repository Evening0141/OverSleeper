using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "SEPlaylist", menuName = "Audio/SE Playlist")]
public class SEPlaylist : ScriptableObject
{
    [Header("SEクリップ一覧（AudioClipのみ）")]
    public List<AudioClip> seClips = new List<AudioClip>();
}