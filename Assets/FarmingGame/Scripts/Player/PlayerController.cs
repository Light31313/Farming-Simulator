using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : BaseAnimationMonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private PlayerAttack attack;
    private Vector2 _moveVector = Vector2.zero;
    private static readonly int Vertical = Animator.StringToHash("Vertical");
    private static readonly int Horizontal = Animator.StringToHash("Horizontal");
    private static readonly int Speed = Animator.StringToHash("Speed");
    private Coroutine _waitDoneAttackingCoroutine;

    private InputMaster.PlayerActions PlayerActions => InputHelper.Input.Player;
    private bool _isAttacking;

    private void FixedUpdate()
    {
        if (_moveVector != Vector2.zero && !_isAttacking)
            rb.MovePosition(rb.position + moveSpeed * Time.fixedDeltaTime * _moveVector);
    }

    private void OnEnable()
    {
        PlayerActions.Movement.performed += OnMovementPerformed;
        PlayerActions.Movement.canceled += OnMovementCancelled;
        PlayerActions.Attack.performed += OnClickAttack;
    }

    private void OnDisable()
    {
        PlayerActions.Movement.performed -= OnMovementPerformed;
        PlayerActions.Movement.canceled -= OnMovementCancelled;
        PlayerActions.Attack.performed -= OnClickAttack;
    }

    public void OnAttackComplete()
    {
        if (PlayerActions.Attack.IsPressed()) return;
        _isAttacking = false;
        SetAnimationState(PlayerState.Idle);
    }

    private void OnMovementPerformed(InputAction.CallbackContext context)
    {
        _waitDoneAttackingCoroutine = StartCoroutine(IEWaitDoneAttacking());
        return;

        IEnumerator IEWaitDoneAttacking()
        {
            yield return new WaitUntil(() => !_isAttacking);
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
        animator.SetFloat(Speed, 0);
    }

    private void OnClickAttack(InputAction.CallbackContext context)
    {
        SetAnimationState(PlayerState.Chop);
        _isAttacking = true;
    }

    public void Chop()
    {
        attack.Chop();
    }

    public void DoneChopping()
    {
        attack.DoneChopping();
    }
}


public static class PlayerState
{
    public const string Idle = "Idle";
    public const string Movement = "Movement";
    public const string Chop = "Chop";
}