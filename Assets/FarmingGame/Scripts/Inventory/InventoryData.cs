using System.Collections.Generic;
using GgAccel;
using UnityEngine;

public class InventoryData : Singleton<InventoryData>
{
    private Dictionary<int, InventoryItem> _itemsDictionary = new();
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
            _itemsDictionary[item.Slot] = item;
        }

        ChangeHoldItem(0);
    }

    public static int Collect(DropItemConfig config, int stack)
    {
        return Instance._Collect(config, stack);
    }

    private int _Collect(DropItemConfig config, int stack)
    {
        foreach (var item in _itemsDictionary)
        {
            if (item.Value.Config.Type != config.Type) continue;
            Debug.Log(
                $"Item: {config.Type}, stack: {_itemsDictionary[item.Key].CurrentStack + stack}, item position: {item.Key}");
            return _itemsDictionary[item.Key].AddStack(stack);
        }

        for (var i = 0; i < NUMBER_OF_SLOT; i++)
        {
            if (_itemsDictionary.ContainsKey(i)) continue;
            _itemsDictionary[i] = new InventoryItem(config, i);
            return _itemsDictionary[i].AddStack(stack);
        }

        return stack;
    }

    public static void ChangeHoldItem(int itemPos)
    {
        Instance._ChangeHoldItem(itemPos);
    }

    private void _ChangeHoldItem(int itemPos)
    {
        CurrentHoldItem = !_itemsDictionary.ContainsKey(itemPos) ? null : _itemsDictionary[itemPos];
        InventorySignals.OnChangeItemHold.Dispatch(itemPos);
    }
}

public class InventoryItem
{
    public readonly DropItemConfig Config;
    public int CurrentStack { get; private set; }
    public int Slot { get; private set; }

    public InventoryItem(DropItemConfig config, int slot)
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