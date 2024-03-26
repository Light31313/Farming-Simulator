using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bayat.SaveSystem;
using strange.extensions.signal.impl;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "TileViewModel", menuName = "ViewModel/TileViewModel")]
public class TileViewModel : ScriptableSingleton<TileViewModel>
{
    public static readonly Signal<Vector3Int, bool> OnWateringTile = new();
    public static readonly Signal<Vector3Int> OnUpdateIndicator = new();
    public static readonly Signal<FarmingTileInfo, Vector3Int> OnSowSeedSuccess = new();
    public static readonly Signal<Vector3Int> OnHoeTileSuccess = new();
    private readonly Dictionary<Vector3Int, FarmingTileInfo> _farmingTileDic = new();
    private readonly Dictionary<Vector3Int, bool> _interactableTiles = new();

    private const string FarmingTileDicKey = "farmingTileDic";

    public Vector3Int CurrentIndicatorPos { get; private set; }

    public bool IsExistFarmingTileInPos(Vector3Int pos)
    {
        return _farmingTileDic.ContainsKey(pos);
    }

    private bool IsExistFarmingTileInCurrentPos()
    {
        return _farmingTileDic.ContainsKey(CurrentIndicatorPos);
    }

    private bool IsExistPlantInCurrentTile()
    {
        return _farmingTileDic[CurrentIndicatorPos].config;
    }

    private bool IsCurrentTileInteractable()
    {
        return _interactableTiles.ContainsKey(CurrentIndicatorPos);
    }

    public void Save()
    {
        SaveSystemAPI.SaveAsync($"{FarmingTileDicKey}key", _farmingTileDic.Keys.ToList());
        SaveSystemAPI.SaveAsync($"{FarmingTileDicKey}value", _farmingTileDic.Values.ToList());
    }

    public async Task<Dictionary<Vector3Int, FarmingTileInfo>> Load()
    {
        var keys = await SaveSystemAPI.LoadAsync<List<Vector3Int>>($"{FarmingTileDicKey}key");
        var values = await SaveSystemAPI.LoadAsync<List<FarmingTileInfo>>($"{FarmingTileDicKey}value");
        _farmingTileDic.Clear();
        for (int i = 0; i < keys.Count; i++)
        {
            if (i < values.Count)
                _farmingTileDic[keys[i]] = values[i];
        }

        return _farmingTileDic;
    }

    public void SowSeed(ItemConfig config, Action onSowSuccess = null)
    {
        if (!IsExistFarmingTileInCurrentPos() || IsExistPlantInCurrentTile()) return;
        _farmingTileDic[CurrentIndicatorPos].InitPlant(config);
        OnSowSeedSuccess.Dispatch(_farmingTileDic[CurrentIndicatorPos], CurrentIndicatorPos);
        onSowSuccess?.Invoke();
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

    public void GrowAllPlantsImmediately()
    {
        foreach (var item in _farmingTileDic)
        {
            item.Value.GrowPlantImmediately();
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

    public void Harvest(Vector3Int pos)
    {
        if (!IsExistFarmingTileInPos(pos)) return;
        _farmingTileDic[pos].Harvest();
        _farmingTileDic.Remove(pos);
    }
}