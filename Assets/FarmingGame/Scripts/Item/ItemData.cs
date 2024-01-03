using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "DropItemData", menuName = "Data/ItemData", order = 1)]
public class ItemData : ScriptableResourcesSingleton<ItemData>
{
    [SerializeField] private List<ItemConfig> items;
    public Dictionary<ItemType, ItemConfig> ItemDictionary;

    private void OnEnable()
    {
        ItemDictionary = items.ToDictionary(item => item.Type);
    }

    private void OnValidate()
    {
        items.Clear();
        items.AddRange(Resources.LoadAll<ItemConfig>("Data/Item/ItemConfigs"));
    }
}