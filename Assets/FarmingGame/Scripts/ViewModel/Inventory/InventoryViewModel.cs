using System;
using System.Collections.Generic;
using strange.extensions.signal.impl;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "InventoryViewModel", menuName = "ViewModel/InventoryViewModel")]
public class InventoryViewModel : ScriptableSingleton<InventoryViewModel>
{
    public static readonly Signal<int> OnChangeItemHold = new();
    public static readonly Signal<int> OnUpdateItem = new();

    [SerializeField] private ItemData itemData;

    public readonly Dictionary<int, InventoryItemData> ItemsDictionary = new();
    private const int NUMBER_OF_SLOT = 54;

    public int CurrentHoldItemPos { get; private set; }
    public InventoryItemData GetCurrentHoldItem => ItemsDictionary[CurrentHoldItemPos];
    
    public ItemConfig GetItemConfig(ItemType type)
    {
        return itemData.ItemDictionary[type];
    }
    
    public void InitInventoryData()
    {
        InitInventory(new List<InventoryItemData>
        {
            new(itemData.ItemDictionary[ItemType.Axe], 0),
            new(itemData.ItemDictionary[ItemType.Hoe], 1),
            new(itemData.ItemDictionary[ItemType.WateringPot], 2),
            new(itemData.ItemDictionary[ItemType.CabbageSeed], 3, 10),
            new(itemData.ItemDictionary[ItemType.CarrotSeed], 4, 10),
        });
    }

    public void InitInventory(List<InventoryItemData> items)
    {
        for (var i = 0; i < NUMBER_OF_SLOT; i++)
        {
            ItemsDictionary[i] = null;
        }

        foreach (var item in items)
        {
            ItemsDictionary[item.Slot] = item;
        }

        ChangeHoldItem(0);
    }

    public int Collect(ItemConfig config, int stack)
    {
        foreach (var item in ItemsDictionary)
        {
            if (item.Value == null || item.Value.Config.Type != config.Type) continue;
            Debug.Log(
                $"Item: {config.Type}, stack: {ItemsDictionary[item.Key].CurrentStack + stack}, item position: {item.Key}");
            OnUpdateItem.Dispatch(item.Key);
            return ItemsDictionary[item.Key].AddStack(stack);
        }

        for (var i = 0; i < NUMBER_OF_SLOT; i++)
        {
            if (ItemsDictionary[i] != null) continue;
            ItemsDictionary[i] = new InventoryItemData(config, i);
            OnUpdateItem.Dispatch(i);
            return ItemsDictionary[i].AddStack(stack);
        }

        return stack;
    }

    public void UseHoldingItem()
    {
        var itemPos = CurrentHoldItemPos;
        GetCurrentHoldItem.UseItem(() =>
        {
            ItemsDictionary[itemPos] = null;
            ChangeHoldItem(itemPos);
        });
        OnUpdateItem.Dispatch(itemPos);
    }

    public void ChangeHoldItem(int itemPos)
    {
        CurrentHoldItemPos = itemPos;
        OnChangeItemHold.Dispatch(itemPos);
    }
}

