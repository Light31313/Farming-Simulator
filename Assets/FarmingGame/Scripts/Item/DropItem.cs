using GgAccel;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    [SerializeField] private SpriteRenderer srItem;
    [SerializeField] private float followSpeed = 8;
    private int _stack;
    public DropItemConfig Config { get; private set; }
    private Transform _player;

    private void FixedUpdate()
    {
        if (!_player) return;
        var itemPos = transform.position;
        var playerPos = _player.position;
        if ((itemPos - playerPos).sqrMagnitude < 0.3f)
        {
            Collect();
        }
        else
        {
            transform.position = Vector3.Lerp(itemPos, playerPos, Time.fixedDeltaTime * followSpeed);
        }
    }

    private void OnDisable()
    {
        _player = null;
    }

    public void UpdateItem(DropItemConfig config, int stack)
    {
        _stack = stack;
        Config = config;
        srItem.sprite = config.SpriteItem;
    }

    private void Collect()
    {
        var overMaxStack = PlayerInventoryData.Collect(Config, _stack);
        if (overMaxStack > 0)
        {
            _stack = overMaxStack;
            _player = null;
        }
        else
        {
            Pool.Release(this);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerCollect"))
        {
            _player = other.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("PlayerCollect"))
        {
            _player = null;
        }
    }
}