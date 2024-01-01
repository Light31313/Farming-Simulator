using System.Collections.Generic;
using GgAccel;
using UnityEngine;

public class InventoryData : Singleton<InventoryData>
{
    public readonly Dictionary<int, InventoryItem> ItemsDictionary = new();
    private const int NUMBER_OF_SLOT = 60;
    
    public InventoryItem CurrentHoldItem { get; private set; }

    public static void InitInventory(List<InventoryItem> items)
    {
        Instance._InitInventory(items);
    }

    private void _InitInventory(List<InventoryItem> items)
    {
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
            if (item.Value.Config.Type != config.Type) continue;
            Debug.Log(
                $"Item: {config.Type}, stack: {ItemsDictionary[item.Key].CurrentStack + stack}, item position: {item.Key}");
            InventorySignals.OnCollectItem.Dispatch(item.Key, ItemsDictionary[item.Key]);
            return ItemsDictionary[item.Key].AddStack(stack);
        }

        for (var i = 0; i < NUMBER_OF_SLOT; i++)
        {
            if (ItemsDictionary.ContainsKey(i)) continue;
            ItemsDictionary[i] = new InventoryItem(config, i);
            InventorySignals.OnCollectItem.Dispatch(i, ItemsDictionary[i]);
            return ItemsDictionary[i].AddStack(stack);
        }
        
        return stack;
    }

    public static void ChangeHoldItem(int itemPos)
    {
        Instance._ChangeHoldItem(itemPos);
    }

    private void _ChangeHoldItem(int itemPos)
    {
        CurrentHoldItem = !ItemsDictionary.ContainsKey(itemPos) ? null : ItemsDictionary[itemPos];
        InventorySignals.OnChangeItemHold.Dispatch(itemPos);
    }
}

public class InventoryItem
{
    public readonly ItemConfig Config;
    public int CurrentStack { get; private set; }
    public int Slot { get; private set; }

    public InventoryItem(ItemConfig config, int slot)
    {
        this.Config = config;
        Slot = slot;
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