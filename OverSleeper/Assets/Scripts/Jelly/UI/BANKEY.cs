using UnityEngine;
using UnityEngine.UI;
public class BANKEY : MonoBehaviour, IChildBehavior
{
    // アニメーション設定
    [SerializeField] Animator anim;
    // 対象のRect
    [SerializeField] RectTransform rct;
    // テキスト表示
    [SerializeField] Text text;
    //オブジェクト座標を元に次のアニメーションが可能かチェック
    const float posY = 550.0f;

    // BANの使用状況
    private bool CanBan = true;

    // クールタイム計算用
    private const float _TIME = 5.0f;
    private float _timer = _TIME;

    // このスクリプトでのみ扱うタイミング
    private bool localCheat = false;


    // インターフェース
    // BAN機能
    public void Execute()
    {
        if (rct.anchoredPosition.y >= posY&&CanBan)
        {
            CanBan = false;
            localCheat = false; // 初期
            text.text = "修正中"; // 初期テキスト
            Debug.Log("BAN起動！");
            anim.Play("CLOSE");

            // チートのチェック
            if (DataRelay.Dr.IsCheat)
            {
                localCheat = true;
            }
            else // いない場合の処理
            {
                localCheat = false;
            }
            FuncChaet();
        }
        else
        {
            Debug.Log("デバッグチェックanim");
        }
    }

    // BANの値を渡し再度使用できるかチェック
    public bool CanBAN
    {
        get { return CanBan; }
    }

    // クールタイム計算
    public void CoolTime()
    {
        _timer -= Time.deltaTime;

        if (_timer<=0)
        {
            // 再セット
            _timer = _TIME;
            CanBan = true;

            anim.Play("OPEN");
        }
    }

    // BANした時の処理
    private void FuncChaet()
    {
        if (localCheat)
        {
            text.color = Color.red;
            text.text = "B.A.N";
            // チートの解除
            DataRelay.Dr.IsCheat = false;
        }
        else
        {
            text.color = Color.white;
            text.text = "失敗";
            DataRelay.Dr.Money = DataRelay.Dr.Money / 2; // 半額に
            DataRelay.Dr.Famous -= 1; // 人気度を一つ落とす
        }
    }
}
