using DG.Tweening;
using GgAccel;
using UnityEngine;
using Random = UnityEngine.Random;

public class Tree : BaseAnimationMonoBehaviour
{
    [SerializeField] AudioClip choppedSfx;
    [SerializeField] private DropItem dropItemPrefab;
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
        item.UpdateItem(DropItemData.GetInstance().ItemDictionary[ItemType.Wood], Random.Range(1, 3));
        item.transform.DOJump(new Vector3(treePos.x + Random.Range(-1.5f, 1.5f), treePos.y + Random.Range(-1f, -0.5f),
            0), 1, 1, 0.3f);
    }
}

public static class TreeState
{
    public const string TREE_IDLE = "TreeIdle";
    public const string CHOPPED = "Chopped";
}