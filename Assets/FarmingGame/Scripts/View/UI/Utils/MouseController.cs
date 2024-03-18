using System.Collections;
using GgAccel;
using UnityEngine;

public class MouseController : MonoSingleton<MouseController>
{
    [SerializeField] private Texture2D textureOpenHandCursor, textureCloseHandCursor;
    [SerializeField] private Texture2D textureDefaultCursor;
    private Coroutine _showInteractableCursorCoroutine;
    private bool IsShowingInteractableCursor => InteractableGameObject;
    public GameObject InteractableGameObject { get; private set; }

    public void ShowInteractableCursor(GameObject go)
    {
        if (InteractableGameObject) return;
        InteractableGameObject = go;
        _showInteractableCursorCoroutine = StartCoroutine(IEShowInteractableCursor());

        IEnumerator IEShowInteractableCursor()
        {
            while (true)
            {
                Cursor.SetCursor(textureOpenHandCursor, Vector2.zero, CursorMode.Auto);
                yield return Helpers.GetWaitForSeconds(0.3f);
                Cursor.SetCursor(textureCloseHandCursor, Vector2.zero, CursorMode.Auto);
                yield return Helpers.GetWaitForSeconds(0.3f);
            }
        }
    }

    public void ShowDefaultCursor()
    {
        if (!IsShowingInteractableCursor) return;
        InteractableGameObject = null;
        if (_showInteractableCursorCoroutine != null) StopCoroutine(_showInteractableCursorCoroutine);
        Cursor.SetCursor(Instance.textureDefaultCursor, Vector2.zero, CursorMode.Auto);
    }
}