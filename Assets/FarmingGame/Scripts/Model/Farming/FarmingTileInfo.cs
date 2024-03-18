using GgAccel.Observable;
using Random = UnityEngine.Random;

public class FarmingTileInfo
{
    private bool _isWatered;
    public ItemConfig Config { get; private set; }
    public readonly Observable<int> CurrentStage = new();
    public readonly Observable<bool> IsHarvested = new();
    private int _currentGrowDay;
    public int HarvestQuantity { get; private set; }

    public void GrowPlant()
    {
        if (_isWatered && Config)
        {
            _currentGrowDay++;
            if (IsHarvestable()) return;
            if (CurrentStage.Value < Config.DayToGrowEachState.Length &&
                _currentGrowDay < Config.DayToGrowEachState[CurrentStage.Value]) return;
            CurrentStage.SetValue(CurrentStage.Value + 1);
            _currentGrowDay = 0;
        }

        _isWatered = false;
    }

    public void GrowPlantImmediately()
    {
        CurrentStage.Value = Config.SpritePlantGrows.Length - 1;
    }

    public void InitPlant(ItemConfig config)
    {
        CurrentStage.SetValue(0);
        _currentGrowDay = 0;
        Config = config;
        HarvestQuantity = Random.Range(config.MinHarvestQuantity, config.MaxHarvestQuantity + 1);
    }

    public bool IsHarvestable()
    {
        return CurrentStage.Value >= Config.SpritePlantGrows.Length - 1;
    }

    public void Water(bool isWater)
    {
        _isWatered = isWater;
    }

    public void Harvest()
    {
        if (!IsHarvestable()) return;
        IsHarvested.SetValue(true);
    }
}