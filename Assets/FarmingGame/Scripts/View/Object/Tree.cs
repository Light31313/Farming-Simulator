using DG.Tweening;
using GgAccel;
using UnityEngine;
using Random = UnityEngine.Random;

public class Tree : BaseAnimationMonoBehaviour
{
    [SerializeField] AudioClip choppedSfx;
    [SerializeField] private DropItem dropItemPrefab;
    [SerializeField] private InventoryViewModel viewModel;
    private Transform _cacheTransform;

    private void Awake()
    {
        _cacheTransform = transform;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var playerInteract = other.gameObject.GetComponent<PlayerInteract>();
        if (playerInteract && playerInteract.HoldItemType == ItemType.Axe)
        {
            Chopped();
        }
    }

    private void Chopped()
    {
        AudioManager.PlaySound(choppedSfx);
        SetAnimationState(TreeState.CHOPPED);
        DropWood();
    }

    private void DropWood()
    {
        var treePos = _cacheTransform.position;
        var item = Pool.Get(dropItemPrefab);
        item.transform.position = treePos;
        item.UpdateItem(viewModel.GetItemConfig(ItemType.Wood), Random.Range(1, 3));
        item.transform.DODropItem();
    }
}

public static class TreeState
{
    public const string TREE_IDLE = "TreeIdle";
    public const string CHOPPED = "Chopped";
}