using UnityEngine;
using UnityEngine.UI;
public class LocalUIManager : MonoBehaviour
{
    private TextSpacer[] spaceCreate; // 空白文字を追加するスクリプト

    //MoneyRelayスクリプト呼び出し用
    MoneyRelay moneyrelay;
    DateRelay daterelay;
    #region UI
    [Header("資金表示のテキスト"), SerializeField] Text moneyText;
    [Header("維持費表示のテキスト"), SerializeField] Text maintainText;
    [Header("年表示のテキスト"), SerializeField] Text yearText;
    [Header("月表示のテキスト"), SerializeField] Text monthText;
    [Header("人気度表示のテキスト"), SerializeField] Text famousText;
    [Header("知名度表示のテキスト"), SerializeField] Text popularText;
    [Header("ユーザー数表示のテキスト"), SerializeField] Text userText;
    #endregion  

    // DataRelayから値を受け取り要素番号とする
    private string[] status_USER = { "減少中","一定","上昇中"};
    private string[] status_POPULAR = { "F","E","D","C","B","A","S"};

    // UnityEditorの処理
    #region DEBUG
    // UIManagerはTitleシーンからの継続させるので
    // ここはデバッグ用に置いてあります
#if UNITY_EDITOR
    private SelectButton[] selButtons; // UIButtonの配列
    GameObject debugBlocker;

    private void Start()
    {
        //Monovi形式じゃないのでnewで代入
        moneyrelay = new MoneyRelay();
        //Monovi形式じゃないのでnewを代入
        daterelay = new DateRelay();
        // 実行時に特定オブジェクトを探す
        debugBlocker = GameObject.Find("UIManager");

        if (debugBlocker != null)
        {
            Debug.Log("処理は実行しません");
            return;  // 処理を止める
        }

        // シーン内の処理を読み込む
        spaceCreate = FindObjectsOfType<TextSpacer>(true);
        // 処理実行
        foreach (var text in spaceCreate)
        {
            text.TextSpace();
        }

        // シーン内のボタン処理を読み込む
        selButtons = FindObjectsOfType<SelectButton>(true);

        // 各SelectButtonのStartを呼び出す
        foreach (var button in selButtons)
        {
            button.ButtonStart();
        }
    }
    private void Update()
    {
        //資金計算処理の実行
        moneyrelay.MoneyGrow();
        //年月処理の実行
        daterelay.DateGrow(); 
        Debug.Log("現在資金" + DataRelay.Dr.Money);
        if (debugBlocker != null)
        {
            Debug.Log("処理は実行しません");
            return;  // 処理を止める
        }
        // ボタンの実装
        if (Input.GetMouseButtonDown(0))
        {

            // 各スクリプトで条件を満たしているものを実行する
            // 条件は各selButtonsの中で判定するものとする
            foreach (var button in selButtons)
            {
                button.ButtonUpdate();
            }
        }
        UIDisp();
    }
#endif
    #endregion


    // Main処理はここから
    // UIManagerで呼び出す形で作成
    public void LocalStart()
    {
        // 空白文字のスクリプト
        #region
        //// シーン内の処理を読み込む
        //spaceCreate = FindObjectsOfType<TextSpacer>(true);
        //// 処理実行
        //foreach (var text in spaceCreate)
        //{
        //    text.TextSpace();
        //}
        #endregion
        // UI更新
        UIDisp();
    }

    public void LocalUpdate()
    {

    }


    // UI表示
    private void UIDisp()
    {
        var data = DataRelay.Dr;
        // 初期の反映
        moneyText.text = data.Money.ToString();
        maintainText.text = data.Maintain.ToString();
        yearText.text = data.Year.ToString();
        monthText.text = data.Month.ToString();
        famousText.text = GenerateStars.Generate(data.Famous);
        popularText.text = status_POPULAR[data.Popular];
        userText.text = status_USER[data.User];
    }
    //テキストカラー変更用
    private void ColDisp()
    {
        if(status_USER[DataRelay.Dr.User] == "上昇中")
        {
            ColorChange.ChangeCol(userText);
        }
    }
}
