using UnityEngine;
using UnityEngine.EventSystems;

public class ItemBase : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler
{
    private Camera cam;
    private bool dragFlag = false;

    public int itemNo = 0; // アイテム番号
    public ItemData data; // 自身のアイテムデータ
    public SpriteRenderer sprite; // 子のスプライト
    int[,] shapeArray;
    int width, height;
    public GameObject childObj; // 子供オブジェクト
    // 0521追加分
    Vector2 dragOffset;      // ドラッグした時の補正位置
    int currentRotate = 0;   // 現在の回転値
    Vector2 defaultOffset;   // 補正用の値
    // 0522追加分
    GameObject gridObj; // グリッドマネージャー
    int gridX, gridY;   // グリッド座標
    float cellSize = 0.5f; // セルのサイズ
    GridMng gridScript; // グリッドマネージャーのスクリプト

    //0602  設置できなかった時のパラメーター保存
    Vector3 originalPos;
    int originalRot;
    Quaternion originalQuat;
    int originalGridX, originalGridY;
    int[,] originalShapeArray;
    int originalWidth, originalHeight;
    bool wasOnGrid;

    void Start()
    {
        gridObj = PuzzleManager.Ins.gridObj;
        gridScript = gridObj.GetComponent<GridMng>();

        // (2x2)の場合 x = 0.25f,y = 0.25f
        // (3x3)の場合 x = 0.5f,y = 0.5f
        // (4x1)の場合 x = 0.75f,y = 0f
        defaultOffset.x = 0.75f;
        defaultOffset.y = 0f;

        cam = Camera.main;

        // アイテムを取得
        data = PuzzleManager.Ins.itemList.itemList[itemNo];
        if(data!=null)
        {
            // 子のスプライト変更
            sprite.sprite = data.sprite;
            // 形状データ取得
            width = data.shape.width;
            height = data.shape.height;
            shapeArray = new int [height,width];

            // 形状の1,0を取得
            for(int y=0;y<height;++y)
            {
                for(int x=0;x<width;++x)
                {
                    shapeArray[y, x] = data.shape.GetCell(x, y);
                }
            }
            // 形状からコライダー作成
            CreateCol();
            // オフセット計算
            float ofs = 0.25f;
            defaultOffset.x = ofs * (width - 1);
            defaultOffset.y = ofs * (height - 1);
            // 画像位置を変更
            CalculateOffset();
        }
    }
    // コライダー作成
    void CreateCol()
    {
        float size = 0.5f;
        float baseX = -(width - 1) * size / 2;
        float baseY = -(height - 1) * size / 2;
        for(int y = 0; y < height; ++y)
        {
            for(int x = 0; x<width; ++x)
            {
                if(shapeArray[y,x]==1) // 1が入っているか
                {
                    // ボックスコライダーを子供に追加
                    BoxCollider2D col =
                        childObj.AddComponent<BoxCollider2D>();
                    // 作成したコライダーのサイズ、場所を設定
                    col.size = new Vector2(size, size);
                    float ofsX = baseX + x * size;
                    float ofsY = baseY + y * size;
                    col.offset = new Vector2(ofsX,ofsY);
                }
            }
        }
    }

    // ドラッグ開始
    public void OnBeginDrag(PointerEventData eventData)
    {
        dragFlag = true;


        // ドラッグ開始位置の補正を計算
        Vector2 mousePos =
            cam.ScreenToWorldPoint(eventData.position);
        dragOffset = (Vector2)transform.position - mousePos;
        //グリッド上のアイテムかチェック
        gridX = Mathf.RoundToInt(transform.position.x / cellSize);
        gridY = Mathf.RoundToInt(transform.position.y / cellSize);
        wasOnGrid = gridScript.OnGridPos(gridX, gridY);

        if(wasOnGrid)
        {
            // グリッド上から、自身のアイテムを消す
            gridScript.RemoveItem(this.gameObject);
        }
        //パラメーター保存
        SaveOriginalPara();
    }

    void SaveOriginalPara()
    {
        originalPos = transform.position;
        originalQuat = transform.rotation;
        originalRot = currentRotate;
        originalShapeArray = (int[,])shapeArray.Clone();
        originalWidth = shapeArray.GetLength(1);
        originalHeight = shapeArray.GetLength(0);
        originalGridX = gridX;
        originalGridY = gridY;
    }

    // ドラッグ中
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 mousePos = cam.ScreenToWorldPoint(eventData.position);
        // オフセットを計算した位置に補正を計算
        Vector2 desirePos = mousePos+dragOffset;
        // グリッドに補正する処理
        gridX = Mathf.RoundToInt(desirePos.x/cellSize);
        gridY = Mathf.RoundToInt(desirePos.y / cellSize);

        transform.position = new Vector2(gridX * cellSize, gridY * cellSize);
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        dragFlag = false;

        Vector2 mousePos =
            cam.ScreenToWorldPoint(eventData.position);
        // 何マス目か計算
        gridX = Mathf.RoundToInt(transform.position.x / cellSize);
        gridY = Mathf.RoundToInt(transform.position.y / cellSize);

        // アイテムがおけるか
        if (gridScript.CheckSetItem((int)gridX, (int)gridY, shapeArray))
        {
            // 設置
            gridScript.SetItem(gameObject.gameObject, (int)gridX, (int)gridY, shapeArray);
            //パズルマネージャーの生成アイテムなら、それを消す
            if(PuzzleManager.Ins.createObj==this.gameObject)
            {
                PuzzleManager.Ins.createObj = null;
            }
        }
        else
        {
            //元の位置に戻す
            ResetOriginalPara();
            //乗っていた場合、再度配置する
            if (wasOnGrid)
            {
                gridScript.SetItem(gameObject, gridX, gridY, shapeArray);
            }

        }
        //グリッドの色をクリア
        gridScript.ClearCellColor();
    }

    void ResetOriginalPara()
    {
        transform.position = originalPos;
        transform.rotation = originalQuat;
        currentRotate = originalRot;
        shapeArray = (int[,])originalShapeArray.Clone();
        width = originalWidth;
        height = originalHeight;
        gridX = originalGridX;
        gridY = originalGridY;
        //画像の位置を変更
        CalculateOffset();
    }


    void Update()
    {
        if(!PuzzleManager.Ins.IsGame)
        {
            this.gameObject.GetComponent<ItemBase>().enabled = false;
        }

        if(dragFlag)
        {
            // 右クリックで時計回り
            if(Input.GetKeyDown(KeyCode.Space))
            {
                // 90度回転
                Rotate90();
            }
            
            // セルのハイライト処理
            gridScript.ClearCellColor();
            gridScript.HighlightCell((int)gridX, (int)gridY, shapeArray);
        }
    }

    void Rotate90()
    {
        // 回転の処理
        // 90度回転(0,90,180,270)
        currentRotate = (currentRotate + 90) % 360;
        // 時計回りなので回転値はマイナスに
        transform.localEulerAngles = new Vector3(0, 0, -currentRotate);
        // 座標を補正
        CalculateOffset();
        // 配列を回転
        ArrayRotate();
    }
    void CalculateOffset()
    {
        Vector2 setOffset = Vector2.zero;
        switch (currentRotate)
        {
            case 0:
                setOffset.x = defaultOffset.x;
                setOffset.y = defaultOffset.y;
                break;
            case 90:
                setOffset.x = -defaultOffset.x;
                setOffset.y = defaultOffset.y;
                break;
            case 180:
                setOffset.x = -defaultOffset.x;
                setOffset.y = -defaultOffset.y;
                break;
            case 270:
                setOffset.x = defaultOffset.x;
                setOffset.y = -defaultOffset.y;
                break;
        }
        childObj.transform.localPosition = setOffset;
    }
    // 配列を回転させる
    void ArrayRotate()
    {
        int w = width;
        int h = height;
        int[,] rotated = new int[w, h]; // 高さと幅が逆の配列
        // 
        for(int y=0;y<h;++y)
        {
            for(int x=0;x<w;++x)
            {
                // 90度回転させた値を入れる
                rotated[(w - 1) - x, y] =
                    shapeArray[y, x];
            }
        }
        // 幅と高さ、配列の入れ替え
        shapeArray = rotated;
        width = h;
        height = w;
    }
}
