using UnityEngine;

// �A�C�e�����X�g�p�̃X�N���v�^�u���I�u�W�F�N�g
[CreateAssetMenu(fileName = "NewItemList", menuName = "MyScriptable/ItemListData")]
public class ItemListData : ScriptableObject
{
    public ItemData[] itemList;
}
