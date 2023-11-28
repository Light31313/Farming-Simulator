using UnityEngine;

namespace GgAccel
{
    public static class TransformExtension
    {
        /**
         *Get direction from this to target object;
         */
        public static Vector3 GetDirection(this Transform transform, Vector3 target)
        {
            return (target - transform.position).normalized;
        }
    }
}


