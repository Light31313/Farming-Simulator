using UnityEngine;

namespace GgAccel
{
    public static class CameraHelper
    {
        private static Camera _camera;

        public static Camera Camera
        {
            get
            {
                if (_camera == null) _camera = Camera.main;
                return _camera;
            }
        }

        public static Vector2 GetWorldPositionOfCanvasElement(RectTransform element)
        {
            RectTransformUtility.ScreenPointToWorldPointInRectangle(element, element.position, Camera, out var result);
            return result;
        }

        public static Vector2 ScreenToWorldPoint(Vector3 position)
        {
            return Camera.ScreenToWorldPoint(position);
        }

        public static void SetOrthographicSize(float size)
        {
            Camera.orthographicSize = size;
        }

        public static Vector2 GetMousePositionInWorldPoint()
        {
            return Camera.ScreenToWorldPoint(Input.mousePosition);
        }
    }
}

