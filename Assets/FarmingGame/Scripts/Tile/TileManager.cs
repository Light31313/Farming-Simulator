using System;
using GgAccel;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class TileManager : MonoSingleton<TileManager>
{
    [SerializeField] private Tilemap interactableMap;
    [SerializeField] private Tile hiddenInteractableTile;
    [SerializeField] private AdvancedRuleTile tileDirt;
    [SerializeField] private AudioClip[] sfxHoes;

    private void Start()
    {
        foreach (var pos in interactableMap.cellBounds.allPositionsWithin)
        {
            var tile = interactableMap.GetTile(pos);
            if (tile)
            {
                interactableMap.SetTile(pos, hiddenInteractableTile);
            }
        }
    }

    public static void SetTileHoed(Vector3Int pos)
    {
        Instance._SetTileHoed(pos);
    }

    private void _SetTileHoed(Vector3Int pos)
    {
        if (IsTileInteractable(pos))
        {
            interactableMap.SetTile(pos, tileDirt);
            interactableMap.SetColor(pos, Color.white);
            AudioManager.PlaySound(sfxHoes[Random.Range(0, sfxHoes.Length)]);
        }
    }

    private bool IsTileInteractable(Vector3Int pos)
    {
        var tile = interactableMap.GetTile(pos);
        return tile;
    }
}