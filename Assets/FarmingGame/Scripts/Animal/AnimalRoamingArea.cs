using System;
using UnityEngine;

public class AnimalRoamingArea : MonoBehaviour
{
    [SerializeField] private Vector2 roamingLength;

    private void OnDrawGizmos()
    {
        DebugUtils.DrawRect(new Rect(transform.position, roamingLength), Color.yellow);
    }
}