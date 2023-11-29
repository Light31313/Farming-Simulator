using System.Collections.Generic;
using GgAccel;
using UnityEngine;

public class PlayerInventoryData : Singleton<PlayerInventoryData>
{
    private Dictionary<int, InventoryItem> _itemsDictionary = new();
    private const int NUMBER_OF_SLOT = 64;

    public static int Collect(DropItemConfig config, int stack)
    {
        return Instance._Collect(config, stack);
    }

    private int _Collect(DropItemConfig config, int stack)
    {
        foreach (var item in _itemsDictionary)
        {
            if (item.Value.config.Type != config.Type) continue;
            Debug.Log($"Item: {config.Type}, stack: {_itemsDictionary[item.Key].CurrentStack + stack}");
            return _itemsDictionary[item.Key].AddStack(stack);
        }

        for (var i = 0; i < NUMBER_OF_SLOT; i++)
        {
            if (_itemsDictionary.ContainsKey(i)) continue;
            _itemsDictionary[i] = new InventoryItem(config);
            return _itemsDictionary[i].AddStack(stack);
        }

        return stack;
    }
}

public class InventoryItem
{
    public readonly DropItemConfig config;
    public int CurrentStack { get; private set; }

    public InventoryItem(DropItemConfig config)
    {
        this.config = config;
    }

    public int AddStack(int stack)
    {
        if (CurrentStack + stack > config.MaxStack)
        {
            var res = stack - (config.MaxStack - CurrentStack);
            CurrentStack = config.MaxStack;
            return res;
        }

        CurrentStack += stack;
        return 0;
    }
}