using System;
using System.Collections;
using GgAccel;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    [SerializeField] private SpriteRenderer srItem;
    [SerializeField] private float followSpeed = 8;
    [SerializeField] private Rigidbody2D rb;
    private int _stack;
    public DropItemConfig Config { get; private set; }
    private Transform _player;
    private Transform _cacheTransform;
    private Coroutine _checkPlayerAroundCoroutine;

    private void Awake()
    {
        _cacheTransform = transform;
    }

    private void OnEnable()
    {
        _checkPlayerAroundCoroutine = StartCoroutine(IECheckPlayerAround());
    }

    private IEnumerator IECheckPlayerAround()
    {
        while (isActiveAndEnabled)
        {
            yield return Helpers.GetWaitForSeconds(0.1f);
            if (!_player) continue;
            var playerPos = _player.position;
            rb.velocity = _cacheTransform.GetDirection(playerPos) * followSpeed;
        }
    }

    private void OnDisable()
    {
        _player = null;
        if (_checkPlayerAroundCoroutine != null) StopCoroutine(_checkPlayerAroundCoroutine);
    }

    public void UpdateItem(DropItemConfig config, int stack)
    {
        _stack = stack;
        Config = config;
        srItem.sprite = config.SpriteItem;
    }

    private void Collect()
    {
        var overMaxStack = InventoryData.Collect(Config, _stack);
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
        else if (other.CompareTag("PlayerCollide"))
        {
            Collect();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("PlayerCollect"))
        {
            _player = null;
            rb.velocity = Vector2.zero;
        }
    }
}