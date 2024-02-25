using System;
using System.Collections.Generic;
using GgAccel;
using GgAccel.Observable;
using strange.extensions.signal.impl;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "TileViewModel", menuName = "ViewModel/TileViewModel")]
public class TileViewModel : ScriptableObject
{
    public static readonly Signal<Vector3Int, bool> OnWateringTile = new();
    public static readonly Signal<Vector3Int> OnUpdateIndicator = new();
    public static readonly Signal<FarmingTileInfo, Vector3Int> OnSowSeedSuccess = new();
    public static readonly Signal<Vector3Int> OnHoeTileSuccess = new();

    private readonly Dictionary<Vector3Int, FarmingTileInfo> _farmingTileDic = new();
    private readonly Dictionary<Vector3Int, bool> _interactableTiles = new();

    public Vector3Int CurrentIndicatorPos { get; private set; }

    public bool IsExistFarmingTileInPos(Vector3Int pos)
    {
        return _farmingTileDic.ContainsKey(pos);
    }

    public bool IsExistFarmingTileInCurrentPos()
    {
        return _farmingTileDic.ContainsKey(CurrentIndicatorPos);
    }

    public bool IsExistPlantInCurrentTile()
    {
        return _farmingTileDic[CurrentIndicatorPos].Config;
    }

    public bool IsCurrentTileInteractable()
    {
        return _interactableTiles.ContainsKey(CurrentIndicatorPos);
    }

    public void SowSeed(ItemConfig config, Action onSowSuccess)
    {
        if (!IsExistFarmingTileInCurrentPos() || IsExistPlantInCurrentTile()) return;
        _farmingTileDic[CurrentIndicatorPos].InitPlant(config);
        OnSowSeedSuccess.Dispatch(_farmingTileDic[CurrentIndicatorPos], CurrentIndicatorPos);
        onSowSuccess.Invoke();
    }

    public void UpdateIndicatorPos(Vector3Int newPos)
    {
        if (CurrentIndicatorPos == newPos) return;
        CurrentIndicatorPos = newPos;
        OnUpdateIndicator.Dispatch(newPos);
    }

    public void WaterTile()
    {
        if (!IsExistFarmingTileInCurrentPos()) return;
        _farmingTileDic[CurrentIndicatorPos].Water(true);
        OnWateringTile.Dispatch(CurrentIndicatorPos, true);
    }

    private void UnWaterTile(Vector3Int pos)
    {
        _farmingTileDic[pos].Water(false);
        OnWateringTile.Dispatch(pos, false);
    }

    public void GrowAllPlants()
    {
        foreach (var item in _farmingTileDic)
        {
            item.Value.GrowPlant();
            UnWaterTile(item.Key);
        }
    }

    public void ClearIndicator()
    {
        if (CurrentIndicatorPos == -Vector3Int.one) return;
        CurrentIndicatorPos = -Vector3Int.one;
        OnUpdateIndicator.Dispatch(CurrentIndicatorPos);
    }

    public void HoeTile()
    {
        if (!IsCurrentTileInteractable() || IsExistFarmingTileInCurrentPos()) return;
        _farmingTileDic[CurrentIndicatorPos] = new FarmingTileInfo();
        OnHoeTileSuccess.Dispatch(CurrentIndicatorPos);
    }

    public void UpdateInteractableTiles(Dictionary<Vector3Int, bool> interactableTiles)
    {
        _interactableTiles.Clear();
        _interactableTiles.AddRange(interactableTiles);
    }
}

public class FarmingTileInfo
{
    private bool _isWatered;
    public ItemConfig Config { get; private set; }
    public readonly Observable<int> CurrentStage = new();
    private int _currentGrowDay;

    public void GrowPlant()
    {
        if (_isWatered && Config)
        {
            _currentGrowDay++;
            if (CurrentStage.Value >= Config.SpritePlantGrows.Length - 1) return;
            if (CurrentStage.Value < Config.DayToGrowEachState.Length &&
                _currentGrowDay < Config.DayToGrowEachState[CurrentStage.Value]) return;
            CurrentStage.SetValue(CurrentStage.Value + 1);
            _currentGrowDay = 0;
        }

        _isWatered = false;
    }

    public void InitPlant(ItemConfig config)
    {
        CurrentStage.SetValue(0);
        _currentGrowDay = 0;
        Config = config;
    }

    public void Water(bool isWater)
    {
        _isWatered = isWater;
    }
}