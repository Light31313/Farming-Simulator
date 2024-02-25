using DG.Tweening;
using UnityEngine;

public static class ExtensionFunction
{
    public static void DODropItem(this Transform transform)
    {
        var pos = transform.position;
        transform.DOJump(new Vector3(pos.x + Random.Range(-1.5f, 1.5f), pos.y + Random.Range(-1f, -0.5f),
            0), 1, 1, 0.3f);
    }
}