using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMng : MonoBehaviour
{
    public int width = 10;        // ��
    public int height = 10;       // ����
    public float cellSize = 1.0f; // �Z���̃T�C�Y��1���j�b�g
    public GameObject cellPre;    // �Z���̃v���t�@�u

    // 0522�ǉ�
    GameObject[,] cells;   // �ړ������Z��������
    GameObject[,] items;   // �Z���ɃA�C�e�������邩
    


    void Start()
    {
        cells = new GameObject[height, width];
        items = new GameObject[height, width];
        for(int y=0; y<height; ++y)
        {
            for(int x=0;x<width;++x)
            {
                // ���W�v�Z���A�Z�����N��
                Vector2 pos = new Vector2(x * cellSize, y * cellSize);
                cells[y,x]=Instantiate(cellPre, pos, Quaternion.identity, transform);
                items[y, x] = null;
            }
        }
        ClearCellColor();
    }


    // �Z���̐F���N���A
    public void ClearCellColor()
    {
        for(int y=0;y<height;y++)
        {
            for(int x=0;x<width;x++)
            {
                // �A�C�e�����ݒu����Ă��邩�ŐF�ύX
                Color setCol = items[y, x] != null ? Color.gray : Color.white;
                SpriteRenderer sp = cells[y, x].GetComponent<SpriteRenderer>();
                sp.color = setCol;
            }
        }
    }
    // �A�C�e�����ݒu�ł��邩�`�F�b�N
    public bool CheckSetItem(int sx,int sy,int[,] shape)
    {
        int shapeH = shape.GetLength(0);
        int shapeW = shape.GetLength(1);

        for(int y=0;y<shapeH;++y)
        {
            for(int x=0;x<shapeW;++x)
            {
                if(shape[y,x]==0){ continue; }
                int gx = sx + x; // �`�F�b�N����O���b�h
                int gy = sy + y; // �`�F�b�N����O���b�h
                // �O���b�h���W���O���b�h��ɑ��݂��邩
                if (!OnGridPos(gx, gy)){ return false; }
                // ���łɃA�C�e�����u����Ă��Ȃ����`�F�b�N
                if (items[gy, gx] != null) { return false; }
            }
        }
        return true; // �S��OK�Ȃ̂Ŕz�u�\
    }
    // �A�C�e����ݒu���鏈��
    public void SetItem(GameObject item,int sx,int sy,int[,] shape)
    {
        int shapeH = shape.GetLength(0);
        int shapeW = shape.GetLength(1);

        for (int y = 0; y < shapeH; ++y)
        {
            for (int x = 0; x < shapeW; x++)
            {
                if (shape[y, x] == 0) { continue; }
                int gx = sx + x; // �`�F�b�N����O���b�h
                int gy = sy + y; // �`�F�b�N����O���b�h
                // �A�C�e����o�^
                items[gy, gx] = item;
            }
        }
    }
    // �A�C�e�����O������
    public void RemoveItem(GameObject item)
    {
        for(int y=0;y<height;++y)
        {
            for(int x=0;x<width;++x)
            {
                // �o�^�σA�C�e�����������A�S�ĊO��
                if (items[y, x] == item)
                {
                    items[y, x] = null;
                }
            }
        }
    }
    // �A�C�e���ړ����̃n�C���C�g����
    public void HighlightCell(int sx,int sy,int[,] shape)
    {
        // �A�C�e�����Z�b�g�ł��邩�`�F�b�N
        bool isSet = CheckSetItem(sx, sy, shape);
        // �Z�b�g�ł����物�F�A�o���Ȃ��������
        Color setcol = isSet == true ? Color.yellow : Color.red;

        // �A�C�e���̌`��̕������Z�����`�F�b�N
        int shapeH = shape.GetLength(0);
        int shapeW = shape.GetLength(1);
        for(int y=0;y<shapeH;++y)
        {
            for(int x=0;x<shapeW;++x)
            {
                if(shape[y,x]==0)
                {
                    // �`��0�Ȃ̂Ŏ�
                    continue;
                }
                int gx = sx + x;
                int gy = sy + y;
                // �`�F�b�N����Z�����O���b�h�����`�F�b�N
                if(gx>=0&&gx<width&&gy>=0&&gy<height)
                {
                    SpriteRenderer sp=
                        cells[gy, gx].GetComponent<SpriteRenderer>();
                    sp.color = setcol;
                }
            }
        }
    }


    void Update()
    {
        
    }

    // �O���b�h��ɑ��݂�����W���`�F�b�N
    public bool OnGridPos(int gx,int gy)
    {
        // �O���b�h���W���z��O�ɂ��邩�`�F�b�N
        if(gx<0||gx>=width||gy<0||gy>=height)
        {
            return false;
        }
        return true;
    }
}
