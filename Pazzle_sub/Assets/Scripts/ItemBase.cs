using UnityEngine;
using UnityEngine.EventSystems;

public class ItemBase : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler
{
    private Camera cam;
    private bool dragFlag = false;

    public int itemNo = 0; // アイテム番号
    private ItemData data; // 自身のアイテムデータ
    public SpriteRenderer sprite; // 子のスプライト
    int[,] shapeArray;
    int width, height;
    public GameObject childObj; // 子供オブジェクト

    //0521追加
    Vector2 dragOffset;         //ドラッグした時の補正位置

    int currentRotate = 0;      //現在の回転値
    Vector2 defaultOffset;      //補正用の値

    void Start()
    {
        //4x1の剣の場合、x=0.75,y=0
        //3x3の場合、x=0.5,y=0.5
        //2x2の場合、x=0.25,y=0.25
        //1x1の場合、x=0,y=0
        //defaultOffset.x = 0.25f;
        //defaultOffset.y = 0.25f;

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
            //オフセット計算
            float ofs = 0.25f;
            defaultOffset.x = ofs * (width - 1);
            defaultOffset.y = ofs * (height - 1);
            //画像の位置を変更
            
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
                if (shapeArray[y, x] == 1) // 1が入っているか
                {
                    // ボックスコライダーを子供に追加
                    BoxCollider2D col =
                        childObj.AddComponent<BoxCollider2D>();
                    // 作成したコライダーのサイズを設定
                    col.size = new Vector2(size, size);
                    // 作成したコライダーの場所を計算
                    float ofsX = x * size;
                    float ofsY = y * size;
                    col.offset = new Vector2(ofsX, ofsY);
                }
            }
        }
    }

    //ドラッグ開始
    public void OnBeginDrag(PointerEventData eventData)
    {
        dragFlag = true;

        //ドラッグ開始位置の補正を計算
        Vector2 mousePos = cam.ScreenToWorldPoint(eventData.position);
        dragOffset = (Vector2)transform.position - mousePos;
    }

    //ドラッグ中
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 mousePos = cam.ScreenToWorldPoint(eventData.position);
        Vector2 desiredPos = mousePos + dragOffset;
        //グリッドに補正する処理
        Vector2 gridPos;
        float cellSize = 0.5f;
        //何マス目にあるかを計算
        gridPos.x = Mathf.RoundToInt(desiredPos.x / cellSize);
        gridPos.y = Mathf.RoundToInt(desiredPos.y / cellSize);
        //サイズをかけて場所を計算
        transform.position = new Vector2(gridPos.x * cellSize, gridPos.y * cellSize);
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        dragFlag = false;
    }
   

    void Update()
    {
        if(dragFlag)
        {
            // 右クリックで時計回り
            if(Input.GetMouseButtonDown(1))
            {
                //90度回転
                Rotate90();
                transform.Rotate(0, 0, -90);
            }
        }
    }

    void Rotate90()
    {
        //回転の処理
        //90度回転(0,90,180,270)
        currentRotate = (currentRotate + 90) % 360;
        //時計回りなので回転値はマイナスに
        transform.localEulerAngles=new Vector3(0, 0, -currentRotate);
        //座標を補正
        CalculateOffset();
    }

    void CalculateOffset()
    {
        Vector2 setOffset = Vector2.zero;
        switch(currentRotate)
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

}
