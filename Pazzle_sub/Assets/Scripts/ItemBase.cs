using UnityEngine;
using UnityEngine.EventSystems;

public class ItemBase : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler
{
    private Camera cam;
    private bool dragFlag = false;

    public int itemNo = 0; // �A�C�e���ԍ�
    private ItemData data; // ���g�̃A�C�e���f�[�^
    public SpriteRenderer sprite; // �q�̃X�v���C�g
    int[,] shapeArray;
    int width, height;
    public GameObject childObj; // �q���I�u�W�F�N�g

    //0521�ǉ�
    Vector2 dragOffset;         //�h���b�O�������̕␳�ʒu

    int currentRotate = 0;      //���݂̉�]�l
    Vector2 defaultOffset;      //�␳�p�̒l

    void Start()
    {
        //4x1�̌��̏ꍇ�Ax=0.75,y=0
        //3x3�̏ꍇ�Ax=0.5,y=0.5
        //2x2�̏ꍇ�Ax=0.25,y=0.25
        //1x1�̏ꍇ�Ax=0,y=0
        //defaultOffset.x = 0.25f;
        //defaultOffset.y = 0.25f;

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
            //�I�t�Z�b�g�v�Z
            float ofs = 0.25f;
            defaultOffset.x = ofs * (width - 1);
            defaultOffset.y = ofs * (height - 1);
            //�摜�̈ʒu��ύX
            
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
                if (shapeArray[y, x] == 1) // 1�������Ă��邩
                {
                    // �{�b�N�X�R���C�_�[���q���ɒǉ�
                    BoxCollider2D col =
                        childObj.AddComponent<BoxCollider2D>();
                    // �쐬�����R���C�_�[�̃T�C�Y��ݒ�
                    col.size = new Vector2(size, size);
                    // �쐬�����R���C�_�[�̏ꏊ���v�Z
                    float ofsX = x * size;
                    float ofsY = y * size;
                    col.offset = new Vector2(ofsX, ofsY);
                }
            }
        }
    }

    //�h���b�O�J�n
    public void OnBeginDrag(PointerEventData eventData)
    {
        dragFlag = true;

        //�h���b�O�J�n�ʒu�̕␳���v�Z
        Vector2 mousePos = cam.ScreenToWorldPoint(eventData.position);
        dragOffset = (Vector2)transform.position - mousePos;
    }

    //�h���b�O��
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 mousePos = cam.ScreenToWorldPoint(eventData.position);
        Vector2 desiredPos = mousePos + dragOffset;
        //�O���b�h�ɕ␳���鏈��
        Vector2 gridPos;
        float cellSize = 0.5f;
        //���}�X�ڂɂ��邩���v�Z
        gridPos.x = Mathf.RoundToInt(desiredPos.x / cellSize);
        gridPos.y = Mathf.RoundToInt(desiredPos.y / cellSize);
        //�T�C�Y�������ďꏊ���v�Z
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
            // �E�N���b�N�Ŏ��v���
            if(Input.GetMouseButtonDown(1))
            {
                //90�x��]
                Rotate90();
                transform.Rotate(0, 0, -90);
            }
        }
    }

    void Rotate90()
    {
        //��]�̏���
        //90�x��](0,90,180,270)
        currentRotate = (currentRotate + 90) % 360;
        //���v���Ȃ̂ŉ�]�l�̓}�C�i�X��
        transform.localEulerAngles=new Vector3(0, 0, -currentRotate);
        //���W��␳
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
