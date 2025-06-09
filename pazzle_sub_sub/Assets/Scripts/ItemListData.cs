using UnityEngine;

// アイテムリスト用のスクリプタブルオブジェクト
[CreateAssetMenu(fileName = "NewItemList", menuName = "MyScriptable/ItemListData")]
public class ItemListData : ScriptableObject
{
    public ItemData[] itemList;
}
