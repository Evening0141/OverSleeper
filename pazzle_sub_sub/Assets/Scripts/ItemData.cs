using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "MyScriptable/ItemData")]
public class ItemData : ScriptableObject
{
    public int score;       //スコア
    public Sprite sprite;   // 絵
    public ShapeData shape; // 形状データ
}
