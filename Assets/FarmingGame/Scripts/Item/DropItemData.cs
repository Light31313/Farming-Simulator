using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "DropItemData", menuName = "Data/ItemData", order = 1)]
public class DropItemData : ScriptableResourcesSingleton<DropItemData>
{
    [SerializeField] private List<DropItemConfig> items;
    public Dictionary<ItemType, DropItemConfig> ItemDictionary;

    private void OnEnable()
    {
        ItemDictionary = items.ToDictionary(item => item.Type);
    }
}

[Serializable]
public class DropItemConfig
{
    [SerializeField] private string itemName;
    public string ItemName => itemName;

    [SerializeField] private string itemDescription;
    public string ItemDescription => itemDescription;

    [SerializeField] private Sprite spriteItem;
    public Sprite SpriteItem => spriteItem;

    [SerializeField] private int maxStack;
    public int MaxStack => maxStack;

    [SerializeField] private ItemType type;
    public ItemType Type => type;
}

public enum ItemType
{
    None,
    Wood,
    Milk,
    Egg,
    Hoe,
    Axe,
    WateringPot,
}