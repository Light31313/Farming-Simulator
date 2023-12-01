using System.Collections;
using System.Collections.Generic;
using GgAccel;
using UnityEngine;

public class Animal : BaseAnimationMonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float minIdleTime = 10f;
    [SerializeField] private float maxIdleTime = 20f;
    [SerializeField] private string movingAnim, turnAroundAnim, defaultIdleAnim;
    [SerializeField] private List<string> idleAnims;

    private Vector2 _roamingPos, _roamingLength;
    private Vector2 _movingPos;
    private Transform _cacheTransform;
    private Coroutine _setNewMovePosCoroutine;
    private bool _isMoving;

    private void Awake()
    {
        _cacheTransform = transform;
    }

    private void Start()
    {
        StartCoroutine(IEStartRoamingAround());
    }

    private IEnumerator IEStartRoamingAround()
    {
        while (isActiveAndEnabled)
        {
            yield return Helpers.GetWaitForSeconds(0.1f);

            if (_cacheTransform.GetMagnitude(_movingPos) > 0.1)
            {
                var direction = _cacheTransform.GetDirection(_movingPos);
                if (direction.x * _cacheTransform.localScale.x < 0)
                {
                    rb.velocity = Vector2.zero;
                    SetAnimationState(turnAroundAnim, () => SetAnimationState(movingAnim));
                }

                if (GetCurrentAnimationClip.name.Equals(movingAnim)) rb.velocity = direction * moveSpeed;
            }
            else if (_isMoving)
            {
                _isMoving = false;
                SetAnimationState(defaultIdleAnim);
                rb.velocity = Vector2.zero;
                var idleTime = Random.Range(minIdleTime, maxIdleTime);
                PlayRandomIdleAnim(idleTime);
                StartCoroutine(SetNewMovingPosition(idleTime));
            }
        }
    }

    public void TurnAround()
    {
        var scale = _cacheTransform.localScale;
        _cacheTransform.localScale = new Vector3(scale.x * -1, scale.y, scale.z);
    }

    private void PlayRandomIdleAnim(float idleTime)
    {
        var remainTime = idleTime;
        while (remainTime > 10)
        {
            var timeToNextIdle = Random.Range(5f, 9f);
            remainTime -= timeToNextIdle;
            StartCoroutine(PlayAnim(remainTime));
        }

        IEnumerator PlayAnim(float timeDelay)
        {
            yield return Helpers.GetWaitForSeconds(timeDelay);
            SetAnimationState(idleAnims[Random.Range(0, idleAnims.Count)]);
        }
    }

    public void SetRoamingArea(Vector2 pos, Vector2 length)
    {
        _roamingPos = pos;
        _roamingLength = length;
        _cacheTransform.position = _movingPos = GetRandomMovePos();
        _isMoving = true;
    }

    private IEnumerator SetNewMovingPosition(float delayTime)
    {
        yield return Helpers.GetWaitForSeconds(delayTime);
        _movingPos = GetRandomMovePos();
        _isMoving = true;
        SetAnimationState(movingAnim);
    }

    private Vector2 GetRandomMovePos()
    {
        return new Vector2(_roamingPos.x + Random.Range(0, _roamingLength.x),
            _roamingPos.y + Random.Range(0, _roamingLength.y));
    }
}