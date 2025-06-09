using UnityEngine;
using UnityEngine.EventSystems;

public class PuzzleManager : MonoBehaviour
{
    // �A�C�e�����X�g������
    public ItemListData itemList; // �A�C�e�����X�g
    public GameObject gridObj;    // �O���b�h
    public GameObject itemObj;    //�N���A�C�e���I�u�W�F�N�g
    public GameObject createObj = null;     //���������I�u�W�F�N�g

    // �V���O���g��
    private static PuzzleManager ins;
    public  static PuzzleManager Ins
    {
        get
        {
            if(ins==null)
            {
                ins = (PuzzleManager)FindObjectOfType(typeof(PuzzleManager));
            }
            return ins;
        }
    }

    private void Awake()
    {
        if (this != Ins) { Destroy(this.gameObject);return; }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    //�e�X�g�ŃA�C�e�����N�����鏈��
    public void ItemBootControl()
    {
        //���������A�C�e�����������̂ݐ����ł���
        if(createObj!=null)
        {
            return;
        }

        //�A�C�e���N��
        //�N�����W
        Vector3 pos = new Vector3(-2, 0, 0);
        //�N��
        GameObject obj = Instantiate(itemObj, pos, Quaternion.identity);
        //�A�C�e���ԍ��������_���Őݒ�
        obj.GetComponent<ItemBase>().itemNo = Random.Range(0, itemList.itemList.Length);
        //�I�u�W�F�N�g��ݒ�
        createObj = obj;
        //�X�y�[�X�L�[�Ń{�^����������Ȃ��悤��
        //(�N���b�N����ƑI�𒆂Ƃ����ݒ�ɂȂ��Ă��܂��̂�)
        EventSystem.current.SetSelectedGameObject(null);
    }
    //�A�C�e������������
    public void ItemDeleteControl()
    {
        //�A�C�e�����������͉������Ȃ�
        if(createObj == null)
        {
            return;
        }
        //�A�C�e��������
        Destroy(createObj.gameObject);
        //�I�u�W�F�N�g���Ȃ���
        createObj = null;
    }
}
