using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : BaseAnimationMonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private PlayerInteract interact;
    private Vector2 _moveVector = Vector2.zero;
    private static readonly int Vertical = Animator.StringToHash("Vertical");
    private static readonly int Horizontal = Animator.StringToHash("Horizontal");
    private static readonly int Speed = Animator.StringToHash("Speed");
    private Coroutine _waitDoneAttackingCoroutine;
    private InventoryData InventoryData => InventoryData.Instance;
    private InputMaster.PlayerActions PlayerActions => InputHelper.Input.Player;
    private Transform _cacheTransform;
    private TileManager TileManager => TileManager.Instance;
    private Vector2Int _currentFacingDirection = Vector2Int.down;
    private ItemType _currentInteractingItemType;
    
    private void Start()
    {
        _cacheTransform = transform;
    }

    private void Update()
    {
        if (InventoryData.CurrentHoldItem != null && InventoryData.CurrentHoldItem.Config.NeedIndicator)
        {
            var currentPos = _cacheTransform.position;
            var indicatorPos = new Vector3Int(Mathf.FloorToInt(currentPos.x + _currentFacingDirection.x * 0.8f),
                Mathf.FloorToInt(currentPos.y + _currentFacingDirection.y / 2f));
            TileManager.SetIndicatorTile(indicatorPos);
        }
        else
        {
            TileManager.ClearIndicator();
        }
    }

    private void FixedUpdate()
    {
        if (_moveVector != Vector2.zero && !interact.isInteracting)
            rb.MovePosition(rb.position + moveSpeed * Time.fixedDeltaTime * _moveVector);
    }

    private void OnEnable()
    {
        PlayerActions.Movement.performed += OnMovementPerformed;
        PlayerActions.Movement.canceled += OnMovementCancelled;
        PlayerActions.Interact.performed += OnClickInteract;
        InventorySignals.OnChangeItemHold.AddListener(OnChangeItemHold);
    }

    private void OnDisable()
    {
        PlayerActions.Movement.performed -= OnMovementPerformed;
        PlayerActions.Movement.canceled -= OnMovementCancelled;
        PlayerActions.Interact.performed -= OnClickInteract;
        InventorySignals.OnChangeItemHold.RemoveListener(OnChangeItemHold);
    }

    private void OnChangeItemHold(int itemPos)
    {
        interact.OnChangeItemHold();
    }

    public void OnInteractComplete()
    {
        if (PlayerActions.Interact.IsPressed()) return;
        interact.isInteracting = false;
        SetAnimationState(PlayerState.Idle);
    }

    private void OnMovementPerformed(InputAction.CallbackContext context)
    {
        _waitDoneAttackingCoroutine = StartCoroutine(IEWaitDoneAttacking());
        return;

        IEnumerator IEWaitDoneAttacking()
        {
            yield return new WaitUntil(() => !interact.isInteracting);
            _moveVector = context.ReadValue<Vector2>();
            SetFacingDirection();
            animator.SetFloat(Horizontal, _moveVector.x);
            animator.SetFloat(Vertical, _moveVector.y);
            animator.SetFloat(Speed, 1);
        }
    }

    private void OnMovementCancelled(InputAction.CallbackContext context)
    {
        if (_waitDoneAttackingCoroutine != null) StopCoroutine(_waitDoneAttackingCoroutine);
        _moveVector = Vector2.zero;
        rb.velocity = Vector2.zero;
        animator.SetFloat(Speed, 0);
    }

    private void OnClickInteract(InputAction.CallbackContext context)
    {
        if (InventoryData.CurrentHoldItem == null) return;
        _currentInteractingItemType = InventoryData.CurrentHoldItem.Config.Type;
        switch (InventoryData.CurrentHoldItem.Config.Type)
        {
            case ItemType.Axe:
                SetInteractingAnimation(PlayerState.Chop);
                break;
            case ItemType.Hoe:
                SetInteractingAnimation(PlayerState.Hoe);
                break;
            case ItemType.WateringPot:
                SetInteractingAnimation(PlayerState.Watering);
                break;
            default:
                break;
        }
    }

    private void SetInteractingAnimation(string animationName)
    {
        interact.isInteracting = true;
        SetAnimationState(animationName);
    }

    public void Interact()
    {
        interact.Interact(_currentInteractingItemType);
    }

    public void DoneInteract()
    {
        interact.DoneInteract();
    }


    private void SetFacingDirection()
    {
        _currentFacingDirection = _moveVector switch
        {
            { x: > 0, y: >= 0 } => Vector2Int.right,
            { x: < 0, y: >= 0 } => Vector2Int.left,
            { x: 0, y: > 0 } => Vector2Int.up,
            _ => Vector2Int.down
        };
    }
}

public static class PlayerState
{
    public const string Idle = "Idle";
    public const string Movement = "Movement";
    public const string Chop = "Chop";
    public const string Watering = "Watering";
    public const string Hoe = "Hoe";
}