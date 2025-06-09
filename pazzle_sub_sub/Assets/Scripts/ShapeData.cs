using UnityEngine;

// �`��f�[�^
// Unity�̃��j���[�ō쐬�ł���悤��`
[CreateAssetMenu(fileName ="NewShape",menuName ="MyScriptable/ShapeData")]
public class ShapeData : ScriptableObject
{
    public int width = 1;  // ��
    public int height = 1; // ����
    public int[] cells;     // �l��ۑ�����z��

    public int GetCell(int x,int y)
    {
        return cells[x + y * width];
    }
    public void SetCell(int x,int y,int value)
    {
        cells[x + y * width] = value;
    }
}
