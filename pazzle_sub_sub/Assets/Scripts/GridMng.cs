using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMng : MonoBehaviour
{
    public int width = 10;        // 幅
    public int height = 10;       // 高さ
    public float cellSize = 1.0f; // セルのサイズは1ユニット
    public GameObject cellPre;    // セルのプレファブ

    // 0522追加
    GameObject[,] cells;   // 移動したセルを入れる
    GameObject[,] items;   // セルにアイテムがあるか
    


    void Start()
    {
        cells = new GameObject[height, width];
        items = new GameObject[height, width];
        for(int y=0; y<height; ++y)
        {
            for(int x=0;x<width;++x)
            {
                // 座標計算し、セルを起動
                Vector2 pos = new Vector2(x * cellSize, y * cellSize);
                cells[y,x]=Instantiate(cellPre, pos, Quaternion.identity, transform);
                items[y, x] = null;
            }
        }
        ClearCellColor();
    }


    // セルの色をクリア
    public void ClearCellColor()
    {
        for(int y=0;y<height;y++)
        {
            for(int x=0;x<width;x++)
            {
                // アイテムが設置されているかで色変更
                Color setCol = items[y, x] != null ? Color.gray : Color.white;
                SpriteRenderer sp = cells[y, x].GetComponent<SpriteRenderer>();
                sp.color = setCol;
            }
        }
    }
    // アイテムが設置できるかチェック
    public bool CheckSetItem(int sx,int sy,int[,] shape)
    {
        int shapeH = shape.GetLength(0);
        int shapeW = shape.GetLength(1);

        for(int y=0;y<shapeH;++y)
        {
            for(int x=0;x<shapeW;++x)
            {
                if(shape[y,x]==0){ continue; }
                int gx = sx + x; // チェックするグリッド
                int gy = sy + y; // チェックするグリッド
                // グリッド座標がグリッド上に存在するか
                if (!OnGridPos(gx, gy)){ return false; }
                // すでにアイテムが置かれていないかチェック
                if (items[gy, gx] != null) { return false; }
            }
        }
        return true; // 全てOKなので配置可能
    }
    // アイテムを設置する処理
    public void SetItem(GameObject item,int sx,int sy,int[,] shape)
    {
        int shapeH = shape.GetLength(0);
        int shapeW = shape.GetLength(1);

        for (int y = 0; y < shapeH; ++y)
        {
            for (int x = 0; x < shapeW; x++)
            {
                if (shape[y, x] == 0) { continue; }
                int gx = sx + x; // チェックするグリッド
                int gy = sy + y; // チェックするグリッド
                // アイテムを登録
                items[gy, gx] = item;
            }
        }
    }
    // アイテムを外す処理
    public void RemoveItem(GameObject item)
    {
        for(int y=0;y<height;++y)
        {
            for(int x=0;x<width;++x)
            {
                // 登録済アイテムを検索し、全て外す
                if (items[y, x] == item)
                {
                    items[y, x] = null;
                }
            }
        }
    }
    // アイテム移動中のハイライト処理
    public void HighlightCell(int sx,int sy,int[,] shape)
    {
        // アイテムがセットできるかチェック
        bool isSet = CheckSetItem(sx, sy, shape);
        // セットできたら黄色、出来なかったら赤
        Color setcol = isSet == true ? Color.yellow : Color.red;

        // アイテムの形状の分だけセルをチェック
        int shapeH = shape.GetLength(0);
        int shapeW = shape.GetLength(1);
        for(int y=0;y<shapeH;++y)
        {
            for(int x=0;x<shapeW;++x)
            {
                if(shape[y,x]==0)
                {
                    // 形状が0なので次
                    continue;
                }
                int gx = sx + x;
                int gy = sy + y;
                // チェックするセルがグリッド内かチェック
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

    // グリッド上に存在する座標かチェック
    public bool OnGridPos(int gx,int gy)
    {
        // グリッド座標が配列外にあるかチェック
        if(gx<0||gx>=width||gy<0||gy>=height)
        {
            return false;
        }
        return true;
    }
}
