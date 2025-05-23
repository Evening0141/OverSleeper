using UnityEngine;
using UnityEditor; // エディタ関連
// ShapeDataのインスペクターをカスタムするという定義
[CustomEditor(typeof(ShapeData))]
public class ShapeDataEditor : Editor
{
    // インスペクターの処理を上書き
    public override void OnInspectorGUI()
    {
        ShapeData shapeData = (ShapeData)target; // 形状データ取得

        EditorGUI.BeginChangeCheck(); // 変更チェック開始

        // 幅と高さをインスペクターに表示・編集
        shapeData.width = EditorGUILayout.IntField("幅", shapeData.width);
        shapeData.height = EditorGUILayout.IntField("高さ", shapeData.height);

        // 配列のサイズを幅、高さに合わせて調整
        int size = shapeData.width * shapeData.height;
        // 形状を作り直す必要があるかチェック（初回かサイズ変更）
        if (shapeData.cells == null || shapeData.cells.Length != size)
        {
            int[] newCells = new int[size];
            if (shapeData.cells != null) // データがある場合、コピー
            {
                for(int i=0;i<Mathf.Min(shapeData.cells.Length,
                    newCells.Length);++i)
                {
                    // コピー
                    newCells[i] = shapeData.cells[i];
                }
            }
            // 作り直した配列に置き換え
            shapeData.cells = newCells;
        }

        // 一次元配列をもとに二次元配列のようなグリッドを作成
        // Unityは↑がプラスなので、それに沿った形を作る
        for(int y=shapeData.height-1;y>=0;y--)
        {
            EditorGUILayout.BeginHorizontal(); // 一行開始
            for(int x=0;x<shapeData.width;++x)
            {
                int index = y * shapeData.width + x;
                Color prevColor = GUI.backgroundColor;
                GUI.backgroundColor = (shapeData.cells[index] == 1 ?
                    Color.gray : Color.white);
                // セルの状態をトグルにする
                if(GUILayout.Button(shapeData.cells[index].ToString(),
                    GUILayout.Width(30), GUILayout.Height(30)))
                {
                    shapeData.cells[index] = (shapeData.cells[index] == 0) ? 1 : 0;
                }
                GUI.backgroundColor = prevColor;
            }
            EditorGUILayout.EndHorizontal(); // 一行終了
        }

        // 変更があったら保存
        if(EditorGUI.EndChangeCheck())
        {
            EditorUtility.SetDirty(shapeData);
        }
    }
}
