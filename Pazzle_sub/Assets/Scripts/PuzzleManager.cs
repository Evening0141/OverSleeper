using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    // �A�C�e�����X�g������
    public ItemListData itemList; // �A�C�e�����X�g
    public GameObject gridObj;    // �O���b�h
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
        if (this != ins) { Destroy(this.gameObject);return; }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
