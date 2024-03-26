using System;
using GgAccel.Observable;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class FarmingTileInfo
{
    public bool isWatered;
    public ItemConfig config;
    public readonly Observable<int> CurrentStage = new();
    public readonly Observable<bool> IsHarvested = new();
    [SerializeField] private int _currentGrowDay;
    public int HarvestQuantity { get; private set; }

    public void GrowPlant()
    {
        if (isWatered && config)
        {
            _currentGrowDay++;
            if (IsHarvestable()) return;
            if (CurrentStage.Value < config.DayToGrowEachState.Length &&
                _currentGrowDay < config.DayToGrowEachState[CurrentStage.Value]) return;
            CurrentStage.SetValue(CurrentStage.Value + 1);
            _currentGrowDay = 0;
        }

        isWatered = false;
    }

    public void GrowPlantImmediately()
    {
        CurrentStage.Value = config.SpritePlantGrows.Length - 1;
    }

    public void InitPlant(ItemConfig conf)
    {
        CurrentStage.SetValue(0);
        _currentGrowDay = 0;
        config = conf;
        HarvestQuantity = Random.Range(conf.MinHarvestQuantity, conf.MaxHarvestQuantity + 1);
    }

    public bool IsHarvestable()
    {
        return config && CurrentStage.Value >= config.SpritePlantGrows.Length - 1;
    }

    public void Water(bool isWater)
    {
        isWatered = isWater;
    }

    public void Harvest()
    {
        if (!IsHarvestable()) return;
        IsHarvested.SetValue(true);
    }
}