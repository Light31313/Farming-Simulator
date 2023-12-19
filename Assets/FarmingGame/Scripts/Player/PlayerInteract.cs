using System;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private Collider2D hit;
    public ItemType HoldItemType { get; private set; } = ItemType.None;

    public void OnChangeItemHold(ItemType itemType)
    {
        HoldItemType = InventoryData.Instance.CurrentHoldItem == null
            ? ItemType.None
            : InventoryData.Instance.CurrentHoldItem.Config.Type;
    }

    public void Interact()
    {
        hit.gameObject.SetActive(true);
    }

    public void DoneInteract()
    {
        hit.gameObject.SetActive(false);
    }
}