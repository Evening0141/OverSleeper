using UnityEngine;

public class GridManager : MonoBehaviour
{
    #region Field
    [Header("幅"),SerializeField] int width = 10;      // 幅
    [Header("高さ"),SerializeField] int height = 10;   // 高さ

    [Header("マスのプレファブ"),SerializeField] 
    GameObject CellPrefab;    // マスのプレファブ

    [Header("グリッド(自分自身)"),SerializeField] 
    Transform gridTransform;  // グリッド(自分自身)

    private GameObject[,] gridCells; // マスの二次元配列
    #endregion

    void Start()
    {
        CreateGrid();  // グリッド作成
    }

    #region Method
    private void CreateGrid() // グリッド作成
    {
        gridCells = new GameObject[width, height]; // 二次元配列の生成

        for(int y=0;y<height;++y)
        {
            for(int x=0;x<width;++x)
            {
                // マスの生成
                GameObject cell = 
                    Instantiate(CellPrefab, gridTransform);
                // 配列に格納
                gridCells[x, y] = cell;
            }
        }
    }
    #endregion
}
