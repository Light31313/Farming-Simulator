using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private Collider2D hit;
    public ItemType HoldItemType { get; private set; } = ItemType.None;
    private InventoryData InventoryData => InventoryData.Instance;
    private Transform _cacheTransform;


    private void Start()
    {
        _cacheTransform = transform;
    }

    public void OnChangeItemHold(ItemType itemType)
    {
        HoldItemType = InventoryData.Instance.CurrentHoldItem == null
            ? ItemType.None
            : InventoryData.Instance.CurrentHoldItem.Config.Type;
    }

    public void Interact()
    {
        switch (InventoryData.CurrentHoldItem.Config.Type)
        {
            case ItemType.Axe:
                hit.gameObject.SetActive(true);
                break;
            case ItemType.Hoe:
                var currentPos = _cacheTransform.position;
                TileManager.SetTileHoed(new Vector3Int((int)currentPos.x, (int)currentPos.y));
                break;
        }
    }

    public void DoneInteract()
    {
        hit.gameObject.SetActive(false);
    }
}