using GgAccel;
using UnityEngine;

public class Tree : BaseAnimationMonoBehaviour
{
    [SerializeField] AudioClip choppedSfx;
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
        GameUtils.Instance.DropItem(treePos, ItemType.Wood, Random.Range(1, 3));
    }
}

public static class TreeState
{
    public const string TREE_IDLE = "TreeIdle";
    public const string CHOPPED = "Chopped";
}