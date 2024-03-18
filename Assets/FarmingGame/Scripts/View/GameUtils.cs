using GgAccel;
using UnityEngine;

public class GameUtils : MonoSingleton<GameUtils>
{
    [SerializeField] private DropItem dropItemPrefab;
    [SerializeField] private ItemData itemData;

    public void DropItem(Vector3 originPos, ItemType type, int quantity)
    {
        var item = Pool.Get(dropItemPrefab);
        item.transform.position = originPos;
        item.UpdateItem(itemData.ItemDictionary[type], quantity);
        item.transform.DODropItem();
    }
}