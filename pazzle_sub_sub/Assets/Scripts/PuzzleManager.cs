using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PuzzleManager : MonoBehaviour
{
    // アイテムリストを持つ
    public ItemListData itemList; // アイテムリスト
    public GameObject gridObj;    // グリッド
    public GameObject itemObj;    //起動アイテムオブジェクト
    public GameObject createObj = null;     //生成したオブジェクト

    public Text timerCmp;           //タイマーのコンポーネント
    public Text scoreCmp;           //スコアのコンポーネント

    public float totalTimer = 4;    //時間

    private bool isGame = true;

    // シングルトン
    private static PuzzleManager ins;
    public static PuzzleManager Ins
    {
        get
        {
            if (ins == null)
            {
                ins = (PuzzleManager)FindObjectOfType(typeof(PuzzleManager));
            }
            return ins;
        }
    }

    private void Awake()
    {
        if (this != Ins) { Destroy(this.gameObject); return; }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(isGame)
        {
            TimerControl();
            ScoreControl();
        }
    }

    private void ScoreControl()
    {
        //コンポーネント取得
        GridMng script = gridObj.GetComponent<GridMng>();
        //スコア計算
        int score = script.CalculationScore();
        scoreCmp.text = "SCORE:" + score.ToString();
    }

    //タイマー管理
    private void TimerControl()
    {
        if (totalTimer > 0)
        {
            totalTimer -= Time.deltaTime;

            if (totalTimer <= 0)
            {
                isGame = false;
                //時間が0になったので終了
                timerCmp.text = "終了";
                return;
            }
        }
        int min = Mathf.FloorToInt(totalTimer / 60);
        int sec = Mathf.FloorToInt(totalTimer % 60);

        float miri = totalTimer % 1.0f;
        //テキスト変更
        timerCmp.text = string.Format("{0:00}:{1:00}:{2:00}", min, sec, Mathf.FloorToInt(miri * 100));
    }


    //テストでアイテムを起動する処理
    public void ItemBootControl()
    {
        //生成したアイテムが無い時のみ生成できる
        if (createObj != null||!IsGame)
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

    public bool IsGame
    {
        get { return isGame; }
    }

    //アイテムを消す処理
    public void ItemDeleteControl()
    {
        //アイテムが無い時は何もしない
        if(createObj == null||!IsGame)
        {
            return;
        }
        //アイテムを消す
        Destroy(createObj.gameObject);
        //オブジェクトをなしに
        createObj = null;
    }
}
