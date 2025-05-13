using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class SelectButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Image selImg; // 選択中に表示するオブジェクト

    // 子オブジェクトのインターフェース取得用
    IChildBehavior child;

    private bool isHolding = false;

    public void OnPointerEnter(PointerEventData eventData)
    {
        // 表示
        selImg.enabled = true;
        isHolding = true;
        Debug.Log("UIに触れた");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // 非表示
        selImg.enabled = false;
        isHolding = false;
        Debug.Log("UIから離れた");
    }

    public void ButtonStart()
    {
        Debug.Log("AAA");
        // 子オブジェクトの中から IChildBehavior を探す
        child = GetComponentInChildren<IChildBehavior>();
    }

    public void ButtonUpdate()
    {
        if (isHolding)
        {
            if (child != null)
            {
                child.Execute();
            }
            else
            {
                Debug.LogWarning("子に IChildBehavior を実装したスクリプトが見つかりません");
            }
        }
    }

}
