using UnityEngine;
using UnityEngine.EventSystems;

public class PuzzleManager : MonoBehaviour
{
    // アイテムリストを持つ
    public ItemListData itemList; // アイテムリスト
    public GameObject gridObj;    // グリッド
    public GameObject itemObj;    //起動アイテムオブジェクト
    public GameObject createObj = null;     //生成したオブジェクト

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

    //テストでアイテムを起動する処理
    public void ItemBootControl()
    {
        //生成したアイテムが無い時のみ生成できる
        if(createObj!=null)
        {
            return;
        }

        //アイテム起動
        //起動座標
        Vector3 pos = new Vector3(-2, 0, 0);
        //起動
        GameObject obj = Instantiate(itemObj, pos, Quaternion.identity);
        //アイテム番号をランダムで設定
        obj.GetComponent<ItemBase>().itemNo = Random.Range(0, itemList.itemList.Length);
        //オブジェクトを設定
        createObj = obj;
        //スペースキーでボタンが押されないように
        //(クリックすると選択中という設定になってしまうので)
        EventSystem.current.SetSelectedGameObject(null);
    }
    //アイテムを消す処理
    public void ItemDeleteControl()
    {
        //アイテムが無い時は何もしない
        if(createObj == null)
        {
            return;
        }
        //アイテムを消す
        Destroy(createObj.gameObject);
        //オブジェクトをなしに
        createObj = null;
    }
}
