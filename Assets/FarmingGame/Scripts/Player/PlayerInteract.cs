using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private Collider2D hit;
    [Header("Watering")] [SerializeField] private AudioClip[] wateringAudioClips;

    [SerializeField] private float wateringVolumeScale = 0.5f;
    [SerializeField] private SpriteRenderer srItemHold;
    public ItemType HoldItemType { get; private set; } = ItemType.None;
    private InventoryData InventoryData => InventoryData.Instance;
    private TileManager TileManager => TileManager.Instance;
    private Transform _cacheTransform;
    [HideInInspector] public bool isInteracting;

    private void Start()
    {
        _cacheTransform = transform;
    }

    private void OnEnable()
    {
        InventorySignals.OnChangeItemHold.AddListener(OnChangeItemHold);
    }

    private void OnDisable()
    {
        InventorySignals.OnChangeItemHold.RemoveListener(OnChangeItemHold);
    }

    private void OnChangeItemHold(int itemPos)
    {
        StartCoroutine(IEWaitDoneInteracting());

        IEnumerator IEWaitDoneInteracting()
        {
            yield return new WaitUntil(() => !isInteracting);
            if (InventoryData.GetCurrentHoldItem == null)
            {
                HoldItemType = ItemType.None;
                srItemHold.enabled = false;
            }
            else
            {
                HoldItemType = InventoryData.GetCurrentHoldItem.Config.Type;
                if (!InventoryData.GetCurrentHoldItem.Config.Tags.Any(itemTag =>
                        itemTag is ItemTag.Tool or ItemTag.Weapon))
                {
                    srItemHold.enabled = true;
                    srItemHold.sprite = InventoryData.GetCurrentHoldItem.Config.SpriteItem;
                }
                else
                {
                    srItemHold.enabled = false;
                }
            }
        }
    }

    public void Interact()
    {
        switch (HoldItemType)
        {
            case ItemType.Axe:
                hit.enabled = true;
                break;
            case ItemType.Hoe:
                TileManager.SetTileHoed();
                break;
            case ItemType.WateringPot:
                TileManager.WaterHoedTile();
                wateringAudioClips.PlayRandomClips(wateringVolumeScale);
                break;
            case ItemType.None:
                break;
            default:
                if (InventoryData.GetCurrentHoldItem.Config.Tags.Any(itemTag => itemTag == ItemTag.Seed))
                {
                    TileManager.SowSeed(InventoryData.GetCurrentHoldItem.Config, InventoryData.UseHoldItem);
                }

                break;
        }
    }

    public void DoneInteract()
    {
        hit.enabled = false;
    }
}