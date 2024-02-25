using System.Collections.Generic;
using GgAccel;
using UnityEngine;

public class RoamingArea : MonoBehaviour
{
    [SerializeField] private Vector2 roamingLength;

    [SerializeField] private List<Animal> animals;
    private Transform _cacheTransform;

    private void Start()
    {
        _cacheTransform = transform;
        foreach (var animalPrefab in animals)
        {
            var animal = Pool.Get(animalPrefab, transform);
            animal.SetRoamingArea(_cacheTransform.position, roamingLength);
        }
    }

    private void OnDrawGizmos()
    {
        DebugUtils.DrawRect(new Rect(transform.position, roamingLength), Color.yellow);
    }
}