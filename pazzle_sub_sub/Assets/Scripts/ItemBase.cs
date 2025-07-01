using UnityEngine;
using UnityEngine.EventSystems;

public class ItemBase : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler
{
    private Camera cam;
    private bool dragFlag = false;

    public int itemNo = 0; // �A�C�e���ԍ�
    public ItemData data; // ���g�̃A�C�e���f�[�^
    public SpriteRenderer sprite; // �q�̃X�v���C�g
    int[,] shapeArray;
    int width, height;
    public GameObject childObj; // �q���I�u�W�F�N�g
    // 0521�ǉ���
    Vector2 dragOffset;      // �h���b�O�������̕␳�ʒu
    int currentRotate = 0;   // ���݂̉�]�l
    Vector2 defaultOffset;   // �␳�p�̒l
    // 0522�ǉ���
    GameObject gridObj; // �O���b�h�}�l�[�W���[
    int gridX, gridY;   // �O���b�h���W
    float cellSize = 0.5f; // �Z���̃T�C�Y
    GridMng gridScript; // �O���b�h�}�l�[�W���[�̃X�N���v�g

    //0602  �ݒu�ł��Ȃ��������̃p�����[�^�[�ۑ�
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

        // (2x2)�̏ꍇ x = 0.25f,y = 0.25f
        // (3x3)�̏ꍇ x = 0.5f,y = 0.5f
        // (4x1)�̏ꍇ x = 0.75f,y = 0f
        defaultOffset.x = 0.75f;
        defaultOffset.y = 0f;

        cam = Camera.main;

        // �A�C�e�����擾
        data = PuzzleManager.Ins.itemList.itemList[itemNo];
        if(data!=null)
        {
            // �q�̃X�v���C�g�ύX
            sprite.sprite = data.sprite;
            // �`��f�[�^�擾
            width = data.shape.width;
            height = data.shape.height;
            shapeArray = new int [height,width];

            // �`���1,0���擾
            for(int y=0;y<height;++y)
            {
                for(int x=0;x<width;++x)
                {
                    shapeArray[y, x] = data.shape.GetCell(x, y);
                }
            }
            // �`�󂩂�R���C�_�[�쐬
            CreateCol();
            // �I�t�Z�b�g�v�Z
            float ofs = 0.25f;
            defaultOffset.x = ofs * (width - 1);
            defaultOffset.y = ofs * (height - 1);
            // �摜�ʒu��ύX
            CalculateOffset();
        }
    }
    // �R���C�_�[�쐬
    void CreateCol()
    {
        float size = 0.5f;
        float baseX = -(width - 1) * size / 2;
        float baseY = -(height - 1) * size / 2;
        for(int y = 0; y < height; ++y)
        {
            for(int x = 0; x<width; ++x)
            {
                if(shapeArray[y,x]==1) // 1�������Ă��邩
                {
                    // �{�b�N�X�R���C�_�[���q���ɒǉ�
                    BoxCollider2D col =
                        childObj.AddComponent<BoxCollider2D>();
                    // �쐬�����R���C�_�[�̃T�C�Y�A�ꏊ��ݒ�
                    col.size = new Vector2(size, size);
                    float ofsX = baseX + x * size;
                    float ofsY = baseY + y * size;
                    col.offset = new Vector2(ofsX,ofsY);
                }
            }
        }
    }

    // �h���b�O�J�n
    public void OnBeginDrag(PointerEventData eventData)
    {
        dragFlag = true;


        // �h���b�O�J�n�ʒu�̕␳���v�Z
        Vector2 mousePos =
            cam.ScreenToWorldPoint(eventData.position);
        dragOffset = (Vector2)transform.position - mousePos;
        //�O���b�h��̃A�C�e�����`�F�b�N
        gridX = Mathf.RoundToInt(transform.position.x / cellSize);
        gridY = Mathf.RoundToInt(transform.position.y / cellSize);
        wasOnGrid = gridScript.OnGridPos(gridX, gridY);

        if(wasOnGrid)
        {
            // �O���b�h�ォ��A���g�̃A�C�e��������
            gridScript.RemoveItem(this.gameObject);
        }
        //�p�����[�^�[�ۑ�
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

    // �h���b�O��
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 mousePos = cam.ScreenToWorldPoint(eventData.position);
        // �I�t�Z�b�g���v�Z�����ʒu�ɕ␳���v�Z
        Vector2 desirePos = mousePos+dragOffset;
        // �O���b�h�ɕ␳���鏈��
        gridX = Mathf.RoundToInt(desirePos.x/cellSize);
        gridY = Mathf.RoundToInt(desirePos.y / cellSize);

        transform.position = new Vector2(gridX * cellSize, gridY * cellSize);
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        dragFlag = false;

        Vector2 mousePos =
            cam.ScreenToWorldPoint(eventData.position);
        // ���}�X�ڂ��v�Z
        gridX = Mathf.RoundToInt(transform.position.x / cellSize);
        gridY = Mathf.RoundToInt(transform.position.y / cellSize);

        // �A�C�e���������邩
        if (gridScript.CheckSetItem((int)gridX, (int)gridY, shapeArray))
        {
            // �ݒu
            gridScript.SetItem(gameObject.gameObject, (int)gridX, (int)gridY, shapeArray);
            //�p�Y���}�l�[�W���[�̐����A�C�e���Ȃ�A���������
            if(PuzzleManager.Ins.createObj==this.gameObject)
            {
                PuzzleManager.Ins.createObj = null;
            }
        }
        else
        {
            //���̈ʒu�ɖ߂�
            ResetOriginalPara();
            //����Ă����ꍇ�A�ēx�z�u����
            if (wasOnGrid)
            {
                gridScript.SetItem(gameObject, gridX, gridY, shapeArray);
            }

        }
        //�O���b�h�̐F���N���A
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
        //�摜�̈ʒu��ύX
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
            // �E�N���b�N�Ŏ��v���
            if(Input.GetKeyDown(KeyCode.Space))
            {
                // 90�x��]
                Rotate90();
            }
            
            // �Z���̃n�C���C�g����
            gridScript.ClearCellColor();
            gridScript.HighlightCell((int)gridX, (int)gridY, shapeArray);
        }
    }

    void Rotate90()
    {
        // ��]�̏���
        // 90�x��](0,90,180,270)
        currentRotate = (currentRotate + 90) % 360;
        // ���v���Ȃ̂ŉ�]�l�̓}�C�i�X��
        transform.localEulerAngles = new Vector3(0, 0, -currentRotate);
        // ���W��␳
        CalculateOffset();
        // �z�����]
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
    // �z�����]������
    void ArrayRotate()
    {
        int w = width;
        int h = height;
        int[,] rotated = new int[w, h]; // �����ƕ����t�̔z��
        // 
        for(int y=0;y<h;++y)
        {
            for(int x=0;x<w;++x)
            {
                // 90�x��]�������l������
                rotated[(w - 1) - x, y] =
                    shapeArray[y, x];
            }
        }
        // ���ƍ����A�z��̓���ւ�
        shapeArray = rotated;
        width = h;
        height = w;
    }
}
