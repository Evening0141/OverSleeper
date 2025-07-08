using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class SelectButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Image selImg; // 選択中に表示するオブジェクト

    // 子オブジェクトのインターフェース取得用
    IChildBehavior child;

    private bool isHolding = false;

    #region Interface
    public void OnPointerEnter(PointerEventData eventData)
    {
        selImg.enabled = true;   // 表示
        isHolding = true;        // 触れている
        // SEセット
        DataRelay.Dr.Data_SE = DataRelay.SE_Name.Enter;
        Debug.Log("UIに触れた");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        selImg.enabled = false;　// 非表示
        isHolding = false;       // 触れていない
        Debug.Log("UIから離れた");
    }
    #endregion

    /// <summary>
    /// これをManagerにて呼び出し実行
    /// </summary>
    public void ButtonStart()
    {
        // 子オブジェクトの中から IChildBehavior を探す
        child = GetComponentInChildren<IChildBehavior>();
    }

    /// <summary>
    /// これをManagerにて呼び出し実行
    /// </summary>
    public void ButtonUpdate()
    {
        if (isHolding)
        {
            if (child != null)
            {
                // SEセット
                DataRelay.Dr.Data_SE = DataRelay.SE_Name.Click;

                // 実行
                child.Execute();
            }
            else
            {
                Debug.LogWarning("子に IChildBehavior を実装したスクリプトが見つかりません");
                Debug.Log(this.gameObject.name);
            }
        }
    }
}
