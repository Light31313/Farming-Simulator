using System;
using UnityEngine;

public class Bed : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerCollide"))
        {
            TimeSignals.OnGoToSleep.Dispatch();
        }
    }
}