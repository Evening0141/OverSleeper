using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMng : MonoBehaviour
{
    public int width = 10;        // 幅
    public int height = 10;       // 高さ
    public float cellSize = 1.0f; // セルのサイズは1ユニット
    public GameObject cellPre;    // セルのプレファブ

    void Start()
    {
        for(int y=0; y<height; ++y)
        {
            for(int x=0;x<width;++x)
            {
                // 座標計算し、セルを起動
                Vector2 pos = new Vector2(x * cellSize, y * cellSize);
                Instantiate(cellPre, pos, Quaternion.identity, transform);
            }
        }
        
    }

    void Update()
    {
        
    }
}
