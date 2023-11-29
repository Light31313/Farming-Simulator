using UnityEngine;

public abstract class BaseAnimationMonoBehaviour : MonoBehaviour
{
    [SerializeField] protected Animator animator;
    protected string _currentState;

    private void OnValidate()
    {
        animator = GetComponent<Animator>();
    }

    protected void SetAnimationState(string newState)
    {
        if (_currentState == newState) return;
        animator.Play(newState);
    }
}