using System;
using System.Collections.Generic;
using GgAccel;
using UnityEngine;

public class InventoryData : Singleton<InventoryData>
{
    public readonly Dictionary<int, InventoryItem> ItemsDictionary = new();
    private const int NUMBER_OF_SLOT = 54;

    public int CurrentHoldItemPos { get; private set; }
    public InventoryItem GetCurrentHoldItem => ItemsDictionary[CurrentHoldItemPos];

    public static void InitInventory(List<InventoryItem> items)
    {
        Instance._InitInventory(items);
    }

    private void _InitInventory(List<InventoryItem> items)
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

    public static int Collect(ItemConfig config, int stack)
    {
        return Instance._Collect(config, stack);
    }

    private int _Collect(ItemConfig config, int stack)
    {
        foreach (var item in ItemsDictionary)
        {
            if (item.Value == null || item.Value.Config.Type != config.Type) continue;
            Debug.Log(
                $"Item: {config.Type}, stack: {ItemsDictionary[item.Key].CurrentStack + stack}, item position: {item.Key}");
            InventorySignals.OnUpdateItem.Dispatch(item.Key);
            return ItemsDictionary[item.Key].AddStack(stack);
        }

        for (var i = 0; i < NUMBER_OF_SLOT; i++)
        {
            if (ItemsDictionary[i] != null) continue;
            ItemsDictionary[i] = new InventoryItem(config, i);
            InventorySignals.OnUpdateItem.Dispatch(i);
            return ItemsDictionary[i].AddStack(stack);
        }

        return stack;
    }

    public static void UseHoldItem()
    {
        var itemPos = Instance.CurrentHoldItemPos;
        Instance.GetCurrentHoldItem.UseItem(() =>
        {
            Instance.ItemsDictionary[itemPos] = null;
            ChangeHoldItem(itemPos);
        });
        InventorySignals.OnUpdateItem.Dispatch(itemPos);
    }

    public static void ChangeHoldItem(int itemPos)
    {
        Instance._ChangeHoldItem(itemPos);
    }

    private void _ChangeHoldItem(int itemPos)
    {
        CurrentHoldItemPos = itemPos;
        InventorySignals.OnChangeItemHold.Dispatch(itemPos);
    }
}

public class InventoryItem
{
    public readonly ItemConfig Config;
    public int CurrentStack { get; private set; }
    public int Slot { get; private set; }

    public InventoryItem(ItemConfig config, int slot, int currentStack = 0)
    {
        Config = config;
        Slot = slot;
        CurrentStack = currentStack;
    }

    public void UseItem(Action onOutOfStack)
    {
        CurrentStack--;
        if (CurrentStack <= 0) onOutOfStack.Invoke();
    }

    public int AddStack(int stack)
    {
        if (CurrentStack + stack > Config.MaxStack)
        {
            var res = stack - (Config.MaxStack - CurrentStack);
            CurrentStack = Config.MaxStack;
            return res;
        }

        CurrentStack += stack;
        return 0;
    }
}