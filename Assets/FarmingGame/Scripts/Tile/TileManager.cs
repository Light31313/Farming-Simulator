using System;
using System.Collections.Generic;
using GgAccel;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class TileManager : MonoSingleton<TileManager>
{
    [SerializeField] private Tilemap interactableMap, indicatorMap;
    [SerializeField] private Tile hiddenInteractableTile;
    [SerializeField] private AnimatedTile indicatorTile;
    [SerializeField] private AdvancedRuleTile tileDirt;
    [SerializeField] private AudioClip[] sfxHoes;
    public Vector3Int CurrentIndicatorPos { get; private set; }
    private Dictionary<Vector3Int, InteractableTileInfo> _interactableTileDic = new();


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

    public void WaterHoedTile()
    {
        if (!_interactableTileDic.ContainsKey(CurrentIndicatorPos)) return;
        WaterTile(CurrentIndicatorPos, true);
        _interactableTileDic[CurrentIndicatorPos].isWatered = true;
        interactableMap.SetColor(CurrentIndicatorPos, new Color(0.8f, 0.8f, 0.8f, 1f));
    }

    public void ClearWaterHoedTile()
    {
        foreach (var item in _interactableTileDic)
        {
            if (!item.Value.isWatered) continue;
            WaterTile(item.Key, false);
        }
    }

    private void WaterTile(Vector3Int tilePos, bool isWatering)
    {
        if (!_interactableTileDic.ContainsKey(tilePos)) return;

        if (isWatering)
        {
            _interactableTileDic[tilePos].isWatered = true;
            interactableMap.SetColor(tilePos, new Color(0.8f, 0.8f, 0.8f, 1f));
        }
        else
        {
            _interactableTileDic[tilePos].isWatered = false;
            interactableMap.SetColor(tilePos, Color.white);
        }
    }

    public void SetTileHoed()
    {
        if (!IsTileInteractable(CurrentIndicatorPos) || _interactableTileDic.ContainsKey(CurrentIndicatorPos)) return;
        interactableMap.SetTile(CurrentIndicatorPos, tileDirt);
        interactableMap.SetColor(CurrentIndicatorPos, Color.white);
        _interactableTileDic[CurrentIndicatorPos] = new InteractableTileInfo();
        sfxHoes.PlayRandomClips();
    }

    public void SetIndicatorTile(Vector3Int pos)
    {
        if (pos == CurrentIndicatorPos) return;
        ClearIndicator();
        indicatorMap.SetTile(pos, indicatorTile);
        CurrentIndicatorPos = pos;
    }

    public void ClearIndicator()
    {
        if (CurrentIndicatorPos == -Vector3Int.one) return;
        CurrentIndicatorPos = -Vector3Int.one;
        indicatorMap.ClearAllTiles();
    }

    private bool IsTileInteractable(Vector3Int pos)
    {
        var tile = interactableMap.GetTile(pos);
        return tile;
    }
}

public class InteractableTileInfo
{
    public bool isWatered;
}