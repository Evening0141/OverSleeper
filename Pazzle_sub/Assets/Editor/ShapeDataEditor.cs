using UnityEngine;
using UnityEditor; // �G�f�B�^�֘A
// ShapeData�̃C���X�y�N�^�[���J�X�^������Ƃ�����`
[CustomEditor(typeof(ShapeData))]
public class ShapeDataEditor : Editor
{
    // �C���X�y�N�^�[�̏������㏑��
    public override void OnInspectorGUI()
    {
        ShapeData shapeData = (ShapeData)target; // �`��f�[�^�擾

        EditorGUI.BeginChangeCheck(); // �ύX�`�F�b�N�J�n

        // ���ƍ������C���X�y�N�^�[�ɕ\���E�ҏW
        shapeData.width = EditorGUILayout.IntField("��", shapeData.width);
        shapeData.height = EditorGUILayout.IntField("����", shapeData.height);

        // �z��̃T�C�Y�𕝁A�����ɍ��킹�Ē���
        int size = shapeData.width * shapeData.height;
        // �`�����蒼���K�v�����邩�`�F�b�N�i���񂩃T�C�Y�ύX�j
        if (shapeData.cells == null || shapeData.cells.Length != size)
        {
            int[] newCells = new int[size];
            if (shapeData.cells != null) // �f�[�^������ꍇ�A�R�s�[
            {
                for(int i=0;i<Mathf.Min(shapeData.cells.Length,
                    newCells.Length);++i)
                {
                    // �R�s�[
                    newCells[i] = shapeData.cells[i];
                }
            }
            // ��蒼�����z��ɒu������
            shapeData.cells = newCells;
        }

        // �ꎟ���z������Ƃɓ񎟌��z��̂悤�ȃO���b�h���쐬
        // Unity�́����v���X�Ȃ̂ŁA����ɉ������`�����
        for(int y=shapeData.height-1;y>=0;y--)
        {
            EditorGUILayout.BeginHorizontal(); // ��s�J�n
            for(int x=0;x<shapeData.width;++x)
            {
                int index = y * shapeData.width + x;
                Color prevColor = GUI.backgroundColor;
                GUI.backgroundColor = (shapeData.cells[index] == 1 ?
                    Color.gray : Color.white);
                // �Z���̏�Ԃ��g�O���ɂ���
                if(GUILayout.Button(shapeData.cells[index].ToString(),
                    GUILayout.Width(30), GUILayout.Height(30)))
                {
                    shapeData.cells[index] = (shapeData.cells[index] == 0) ? 1 : 0;
                }
                GUI.backgroundColor = prevColor;
            }
            EditorGUILayout.EndHorizontal(); // ��s�I��
        }

        // �ύX����������ۑ�
        if(EditorGUI.EndChangeCheck())
        {
            EditorUtility.SetDirty(shapeData);
        }
    }
}
