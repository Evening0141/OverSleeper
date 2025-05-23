using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    // アイテムリストを持つ
    public ItemListData itemList; // アイテムリスト
    public GameObject gridObj;    // グリッド
    // シングルトン
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
