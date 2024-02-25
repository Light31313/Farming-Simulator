using GgAccel;
using UnityEngine;

public class Plant : MonoBehaviour
{
    [SerializeField] private SpriteRenderer srPlant;
    private FarmingTileInfo _seed;

    public void InitPlant(FarmingTileInfo seed)
    {
        _seed = seed;
        srPlant.sprite = seed.Config.SpritePlantGrows[0];
        _seed.CurrentStage.OnChangedValues += OnPlantStageChange;
    }

    private void OnDisable()
    {
        _seed.CurrentStage.OnChangedValues -= OnPlantStageChange;
    }

    private void OnPlantStageChange(int oldValue, int newValue)
    {
        srPlant.sprite = _seed.Config.SpritePlantGrows[newValue];
    }

    public void Harvest()
    {
        Pool.Release(this);
    }
}