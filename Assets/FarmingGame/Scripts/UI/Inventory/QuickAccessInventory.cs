using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickAccessInventory : MonoBehaviour
{
    [SerializeField] private InventoryItemHolder[] inventoryItemHolders;
    private Dictionary<int, InventoryItem> ItemsDictionary => InventoryData.Instance.ItemsDictionary;

    private void Start()
    {
        StartCoroutine(WaitInitDataDone());

        IEnumerator WaitInitDataDone()
        {
            yield return new WaitUntil(() => GamePlayModel.Instance.isInitDataDone);
            for (var i = 0; i < inventoryItemHolders.Length; i++)
            {
                var itemView = inventoryItemHolders[i];
                itemView.UpdateItem(ItemsDictionary[i]);
            }
        }
    }

    private void OnValidate()
    {
        inventoryItemHolders = GetComponentsInChildren<InventoryItemHolder>();
        for (var i = 0; i < inventoryItemHolders.Length; i++)
        {
            var itemView = inventoryItemHolders[i];
            itemView.itemPos = i;
        }
    }

    private void OnEnable()
    {
        InventorySignals.OnChangeItemHold.AddListener(OnChangeItemHold);
        InventorySignals.OnUpdateItem.AddListener(OnCollectItem);
    }

    private void OnDisable()
    {
        InventorySignals.OnChangeItemHold.RemoveListener(OnChangeItemHold);
        InventorySignals.OnUpdateItem.RemoveListener(OnCollectItem);
    }

    private void OnCollectItem(int itemPos)
    {
        if (itemPos < inventoryItemHolders.Length)
            inventoryItemHolders[itemPos]
                .UpdateItem(ItemsDictionary[itemPos]);
    }

    private void OnChangeItemHold(int slotPos)
    {
        inventoryItemHolders[slotPos].ChooseItem();
    }
}