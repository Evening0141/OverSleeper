using UnityEngine;

[CreateAssetMenu(fileName = "TextData", menuName = "Hint/TextData", order = 1)]
public class TextData : ScriptableObject
{
    [TextArea(2, 5)]
    public string[] hintMessages;
}