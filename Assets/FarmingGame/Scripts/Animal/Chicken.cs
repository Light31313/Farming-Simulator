using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chicken : BaseAnimationMonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    private Vector2 _roamingPos, _roamingLength;

    private void Start()
    {
    }

    public void SetRoamingArea(Vector2 pos, Vector2 length)
    {
        _roamingPos = pos;
        _roamingLength = length;
    }

    private IEnumerator IERoamingAround()
    {
        while (isActiveAndEnabled)
        {
            yield return null;
        }
    }
}