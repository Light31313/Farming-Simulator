using System.Collections;
using System.Linq;
using GgAccelSDK.Script;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private Collider2D hit;
    [Header("Watering")] [SerializeField] private AudioClip[] wateringAudioClips;

    [SerializeField] private float wateringVolumeScale = 0.5f;
    [SerializeField] private SpriteRenderer srItemHold;
    [SerializeField] private InventoryViewModel inventoryViewModel;
    [SerializeField] private TileViewModel tileViewModel;
    public ItemType HoldItemType { get; private set; } = ItemType.None;
    private Transform _cacheTransform;
    [HideInInspector] public bool isInteracting;

    private void Start()
    {
        _cacheTransform = transform;
    }

    private void OnEnable()
    {
        InventoryViewModel.OnChangeItemHold.AddListener(OnChangeItemHold);
    }

    private void OnDisable()
    {
        InventoryViewModel.OnChangeItemHold.RemoveListener(OnChangeItemHold);
    }

    private void OnChangeItemHold(int itemPos)
    {
        StartCoroutine(IEWaitDoneInteracting());

        IEnumerator IEWaitDoneInteracting()
        {
            yield return new WaitUntil(() => !isInteracting);
            if (inventoryViewModel.GetCurrentHoldItem == null)
            {
                HoldItemType = ItemType.None;
                srItemHold.enabled = false;
            }
            else
            {
                HoldItemType = inventoryViewModel.GetCurrentHoldItem.Config.Type;
                if (!inventoryViewModel.GetCurrentHoldItem.Config.Tags.Any(itemTag =>
                        itemTag is ItemTag.Tool or ItemTag.Weapon))
                {
                    srItemHold.enabled = true;
                    srItemHold.sprite = inventoryViewModel.GetCurrentHoldItem.Config.SpriteItem;
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
                tileViewModel.HoeTile();
                break;
            case ItemType.WateringPot:
                tileViewModel.WaterTile();
                wateringAudioClips.PlayRandomClips(wateringVolumeScale);
                break;
            case ItemType.None:
                break;
            default:
                if (inventoryViewModel.GetCurrentHoldItem.Config.Tags.Any(itemTag => itemTag == ItemTag.Seed))
                {
                    tileViewModel.SowSeed(inventoryViewModel.GetCurrentHoldItem.Config, inventoryViewModel.UseHoldingItem);
                }

                break;
        }
    }

    public void DoneInteract()
    {
        hit.enabled = false;
    }
}