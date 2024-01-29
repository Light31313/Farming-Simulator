using System;
using System.Collections.Generic;
using GgAccel;
using GgAccelSDK.Script;
using Sirenix.Utilities;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class TileManager : MonoSingleton<TileManager>
{
    [SerializeField] private Plant plantPrefab;

    [SerializeField] private Tilemap interactableMap, indicatorMap;
    [SerializeField] private Tile hiddenInteractableTile;
    [SerializeField] private AnimatedTile indicatorTile;
    [SerializeField] private AdvancedRuleTile tileDirt;
    [SerializeField] private AudioClip[] sfxHoes;
    [SerializeField] private AudioClip[] sfxSowSeeds;
    public Vector3Int CurrentIndicatorPos { get; private set; }
    private Dictionary<Vector3Int, HoedTileInfo> _interactableTileDic = new();


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

    private void OnEnable()
    {
        TimeSignals.OnStartNewDay.AddListener(OnStartNewDay);
    }

    private void OnDisable()
    {
        TimeSignals.OnStartNewDay.RemoveListener(OnStartNewDay);
    }

    private void OnStartNewDay(DayData dayData)
    {
        GrowAllPlants();
    }

    public void WaterHoedTile()
    {
        if (!_interactableTileDic.ContainsKey(CurrentIndicatorPos)) return;
        WaterTile(CurrentIndicatorPos, true);
        _interactableTileDic[CurrentIndicatorPos].Water(true);
        interactableMap.SetColor(CurrentIndicatorPos, new Color(0.8f, 0.8f, 0.8f, 1f));
    }

    private void GrowAllPlants()
    {
        foreach (var item in _interactableTileDic)
        {
            item.Value.GrowPlant();
            WaterTile(item.Key, false);
        }
    }

    private void WaterTile(Vector3Int tilePos, bool isWatering)
    {
        if (!_interactableTileDic.ContainsKey(tilePos)) return;

        if (isWatering)
        {
            _interactableTileDic[tilePos].Water(true);
            interactableMap.SetColor(tilePos, new Color(0.8f, 0.8f, 0.8f, 1f));
        }
        else
        {
            _interactableTileDic[tilePos].Water(false);
            interactableMap.SetColor(tilePos, Color.white);
        }
    }

    public void SetTileHoed()
    {
        if (!IsTileInteractable(CurrentIndicatorPos) || _interactableTileDic.ContainsKey(CurrentIndicatorPos)) return;
        interactableMap.SetTile(CurrentIndicatorPos, tileDirt);
        interactableMap.SetColor(CurrentIndicatorPos, Color.white);
        _interactableTileDic[CurrentIndicatorPos] = new HoedTileInfo();
        sfxHoes.PlayRandomClips();
    }

    public void SowSeed(ItemConfig seed, Action onSowSuccess)
    {
        if (!_interactableTileDic.ContainsKey(CurrentIndicatorPos) ||
            _interactableTileDic[CurrentIndicatorPos].Plant != null) return;

        _interactableTileDic[CurrentIndicatorPos].Plant = Pool.Get(plantPrefab, interactableMap.transform);
        _interactableTileDic[CurrentIndicatorPos].Plant.transform.position =
            new Vector3(CurrentIndicatorPos.x + 0.5f, CurrentIndicatorPos.y + 0.4f, 0);
        _interactableTileDic[CurrentIndicatorPos].Plant.InitPlant(seed);
        sfxSowSeeds.PlayRandomClips();
        onSowSuccess.Invoke();
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

public class HoedTileInfo
{
    private bool _isWatered;
    public Plant Plant;

    public void GrowPlant()
    {
        if (_isWatered && Plant)
        {
            Plant.Grow();
        }

        _isWatered = false;
    }

    public void Water(bool isWater)
    {
        _isWatered = isWater;
    }
}