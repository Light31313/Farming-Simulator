using System.Collections;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private Collider2D hit;
    [Header("Watering")]
    [SerializeField] private AudioClip[] wateringAudioClips;

    [SerializeField] private float wateringVolumeScale = 0.5f;
    public ItemType HoldItemType { get; private set; } = ItemType.None;
    private InventoryData InventoryData => InventoryData.Instance;
    private TileManager TileManager => TileManager.Instance;
    private Transform _cacheTransform;
    [HideInInspector] public bool isInteracting;

    private void Start()
    {
        _cacheTransform = transform;
    }

    public void OnChangeItemHold()
    {
        StartCoroutine(IEWaitDoneInteracting());

        IEnumerator IEWaitDoneInteracting()
        {
            yield return new WaitUntil(() => !isInteracting);
            HoldItemType = InventoryData.Instance.CurrentHoldItem == null
                ? ItemType.None
                : InventoryData.Instance.CurrentHoldItem.Config.Type;
        }
    }

    public void Interact(ItemType type)
    {
        switch (type)
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
        }
    }

    public void DoneInteract()
    {
        hit.enabled = false;
    }
}