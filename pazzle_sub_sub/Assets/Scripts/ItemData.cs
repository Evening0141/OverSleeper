using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "MyScriptable/ItemData")]
public class ItemData : ScriptableObject
{
    public int score;       //�X�R�A
    public Sprite sprite;   // �G
    public ShapeData shape; // �`��f�[�^
}
