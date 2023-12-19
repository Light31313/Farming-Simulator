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
    private bool _isInteracting;

    private void FixedUpdate()
    {
        if (_moveVector != Vector2.zero && !_isInteracting)
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
        interact.OnChangeItemHold(InventoryData.CurrentHoldItem == null
            ? ItemType.None
            : InventoryData.CurrentHoldItem.Config.Type);
    }

    public void OnInteractComplete()
    {
        if (PlayerActions.Interact.IsPressed()) return;
        _isInteracting = false;
        SetAnimationState(PlayerState.Idle);
    }

    private void OnMovementPerformed(InputAction.CallbackContext context)
    {
        _waitDoneAttackingCoroutine = StartCoroutine(IEWaitDoneAttacking());
        return;

        IEnumerator IEWaitDoneAttacking()
        {
            yield return new WaitUntil(() => !_isInteracting);
            _moveVector = context.ReadValue<Vector2>();
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
        switch (InventoryData.CurrentHoldItem.Config.Type)
        {
            case ItemType.Axe:
                SetAnimationState(PlayerState.Chop);
                break;
            case ItemType.Hoe:
                SetAnimationState(PlayerState.Hoe);
                break;
            case ItemType.WateringPot:
                SetAnimationState(PlayerState.Watering);
                break;
            default:
                break;
        }


        _isInteracting = true;
    }

    public void Interact()
    {
        interact.Interact();
    }

    public void DoneInteract()
    {
        interact.DoneInteract();
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