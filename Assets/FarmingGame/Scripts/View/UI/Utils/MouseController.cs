using System;
using System.Collections;
using GgAccel;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseController : MonoSingleton<MouseController>
{
    [SerializeField] private Texture2D textureOpenHandCursor, textureCloseHandCursor;
    [SerializeField] private Texture2D textureDefaultCursor;
    private Coroutine _showInteractableCursorCoroutine;
    private bool _isShowingInteractableCursor;
    public bool IsMouseOverUI { get; private set; }

    private void Update()
    {
        IsMouseOverUI = EventSystem.current.IsPointerOverGameObject();
    }

    public void ShowInteractableCursor()
    {
        if (_isShowingInteractableCursor) return;
        _isShowingInteractableCursor = true;
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
        if (!_isShowingInteractableCursor) return;
        _isShowingInteractableCursor = false;
        if (_showInteractableCursorCoroutine != null) StopCoroutine(_showInteractableCursorCoroutine);
        Cursor.SetCursor(Instance.textureDefaultCursor, Vector2.zero, CursorMode.Auto);
    }
}