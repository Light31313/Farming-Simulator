using System.Collections;
using Bayat.SaveSystem;
using GgAccel;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerController : BaseAnimationMonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float harvestRadius = 1f;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private PlayerInteract interact;
    [SerializeField] private InventoryViewModel inventoryViewModel;
    [SerializeField] private PlayerViewModel playerViewModel;
    [SerializeField] private TileViewModel tileViewModel;
    private Vector2 _moveVector = Vector2.zero;
    private static readonly int Vertical = Animator.StringToHash("Vertical");
    private static readonly int Horizontal = Animator.StringToHash("Horizontal");
    private static readonly int Speed = Animator.StringToHash("Speed");
    private Coroutine _waitDoneAttackingCoroutine;
    private InputMaster.PlayerActions PlayerActions => InputHelper.Input.Player;
    private Vector2Int _currentFacingDirection = Vector2Int.down;
    private Transform _cacheTransform;
    

    private void Awake()
    {
        _cacheTransform = transform;
        playerViewModel.UpdateTransform(_cacheTransform);
    }

    private void Update()
    {
        if (inventoryViewModel.GetCurrentHoldItem != null && inventoryViewModel.GetCurrentHoldItem.Config.NeedIndicator)
        {
            var currentPos = playerViewModel.PlayerTransform.position;
            var indicatorPos = new Vector3Int(Mathf.FloorToInt(currentPos.x + _currentFacingDirection.x * 0.8f),
                Mathf.FloorToInt(currentPos.y + _currentFacingDirection.y / 2f));
            tileViewModel.UpdateIndicatorPos(indicatorPos);
        }
        else
        {
            tileViewModel.ClearIndicator();
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
    }

    private void OnDisable()
    {
        PlayerActions.Movement.performed -= OnMovementPerformed;
        PlayerActions.Movement.canceled -= OnMovementCancelled;
        PlayerActions.Interact.performed -= OnClickInteract;
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
        if (MouseController.Instance.IsMouseOverUI) return;
        if (GameObjectInteract()) return;
        InventoryInteract();

        void InventoryInteract()
        {
            if (inventoryViewModel.GetCurrentHoldItem == null) return;
            switch (inventoryViewModel.GetCurrentHoldItem.Config.Type)
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
                    Interact();
                    break;
            }
        }

        bool GameObjectInteract()
        {
            var rayHit =
                Physics2D.GetRayIntersection(CameraHelper.ScreenPointToRay(Mouse.current.position.ReadValue()));
            if (!rayHit.collider) return false;
            var rayHitTransform = rayHit.transform;

            if (rayHitTransform.CompareTag("Plant") &&
                (rayHitTransform.position - _cacheTransform.position).sqrMagnitude <= harvestRadius)
            {
                MouseController.Instance.ShowDefaultCursor();
                var plant = rayHitTransform.GetComponent<Plant>();
                plant.Harvest();
                return true;
            }

            return false;
        }
    }

    private void SetInteractingAnimation(string animationName)
    {
        interact.isInteracting = true;
        SetAnimationState(animationName);
    }

    public void Interact()
    {
        interact.Interact();
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