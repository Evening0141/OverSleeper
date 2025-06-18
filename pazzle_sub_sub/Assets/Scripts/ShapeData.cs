using UnityEngine;

// 形状データ
// Unityのメニューで作成できるよう定義
[CreateAssetMenu(fileName ="NewShape",menuName ="MyScriptable/ShapeData")]
public class ShapeData : ScriptableObject
{
    public int width = 1;  // 幅
    public int height = 1; // 高さ
    public int[] cells;     // 値を保存する配列

    public int GetCell(int x,int y)
    {
        return cells[x + y * width];
    }
    public void SetCell(int x,int y,int value)
    {
        cells[x + y * width] = value;
    }
}
