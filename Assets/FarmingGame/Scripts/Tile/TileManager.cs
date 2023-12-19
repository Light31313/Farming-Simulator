using System;
using GgAccel;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager : MonoSingleton<TileManager>
{
    [SerializeField] private Tilemap interactableMap;
    [SerializeField] private Tile hiddenInteractableTile;

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

    public bool IsTileInteractable(Vector3Int pos)
    {
        var tile = interactableMap.GetTile(pos);
        return tile;
    }
}