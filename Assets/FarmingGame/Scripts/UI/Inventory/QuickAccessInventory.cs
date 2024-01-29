using System.Collections;
using System.Collections.Generic;
using GgAccel;
using UnityEngine;

public class QuickAccessInventory : MonoBehaviour
{
    [SerializeField] private InventoryItemHolder[] inventoryItemHolders;
    private Dictionary<int, InventoryItem> ItemsDictionary => InventoryData.Instance.ItemsDictionary;

    [SerializeField] private AudioClip clickButtonSfx;

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
                var i1 = i;
                itemView.InitToggleEvent((isOn) =>
                {
                    Debug.Log("Call toggle event");
                    if (!isOn) return;
                    OnItemToggleClick(i1);
                });
            }
        }
    }

    private void OnValidate()
    {
        inventoryItemHolders = GetComponentsInChildren<InventoryItemHolder>();
    }

    private void OnEnable()
    {
        InventorySignals.OnUpdateItem.AddListener(OnCollectItem);
        InputSignals.OnQuickAccessInventoryClick.AddListener(OnQuickAccessInventoryClick);
    }

    private void OnDisable()
    {
        InventorySignals.OnUpdateItem.RemoveListener(OnCollectItem);
        InputSignals.OnQuickAccessInventoryClick.RemoveListener(OnQuickAccessInventoryClick);
    }

    private void OnCollectItem(int itemPos)
    {
        if (itemPos < inventoryItemHolders.Length)
            inventoryItemHolders[itemPos]
                .UpdateItem(ItemsDictionary[itemPos]);
    }

    private void OnQuickAccessInventoryClick(int itemPos)
    {
        inventoryItemHolders[itemPos].ChooseItem();
    }

    private void OnItemToggleClick(int itemPos)
    {
        if (itemPos != InventoryData.Instance.CurrentHoldItemPos)
        {
            AudioManager.PlaySound(clickButtonSfx);
        }

        InventoryData.ChangeHoldItem(itemPos);
    }
}