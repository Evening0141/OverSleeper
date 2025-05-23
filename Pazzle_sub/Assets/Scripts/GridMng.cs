using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMng : MonoBehaviour
{
    public int width = 10;        // ��
    public int height = 10;       // ����
    public float cellSize = 1.0f; // �Z���̃T�C�Y��1���j�b�g
    public GameObject cellPre;    // �Z���̃v���t�@�u

    void Start()
    {
        for(int y=0; y<height; ++y)
        {
            for(int x=0;x<width;++x)
            {
                // ���W�v�Z���A�Z�����N��
                Vector2 pos = new Vector2(x * cellSize, y * cellSize);
                Instantiate(cellPre, pos, Quaternion.identity, transform);
            }
        }
        
    }

    void Update()
    {
        
    }
}
