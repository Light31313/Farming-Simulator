using System;
using System.Collections;
using GgAccel;
using UnityEngine;

public abstract class BaseAnimationMonoBehaviour : MonoBehaviour
{
    [SerializeField] protected Animator animator;
    protected string _currentState;

    protected AnimationClip GetCurrentAnimationClip => animator.GetCurrentAnimatorClipInfo(0)[0].clip;

    private void OnValidate()
    {
        animator = GetComponent<Animator>();
    }

    protected void SetAnimationState(string newState, Action onAnimationEnd = null)
    {
        if (_currentState == newState) return;
        animator.Play(newState);
        var clip = animator.GetCurrentAnimatorClipInfo(0)[0].clip;
        if (!clip.isLooping) StartCoroutine(IEOnAnimationEnd());

        IEnumerator IEOnAnimationEnd()
        {
            yield return Helpers.GetWaitForSeconds(clip.length);
            onAnimationEnd?.Invoke();
        }
    }
}