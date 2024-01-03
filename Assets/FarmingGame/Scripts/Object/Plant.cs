using GgAccel;
using UnityEngine;

public class Plant : MonoBehaviour
{
    [SerializeField] private SpriteRenderer srPlant;
    private ItemConfig _seed;
    private int _currentStage;
    private int _currentGrowDay;

    public void InitPlant(ItemConfig seed)
    {
        _seed = seed;
        _currentStage = 0;
        _currentGrowDay = 0;
        srPlant.sprite = seed.SpritePlantGrows[0];
    }

    public void Grow()
    {
        _currentGrowDay++;
        if (_currentStage >= _seed.SpritePlantGrows.Length - 1) return;
        if (_currentStage < _seed.DayToGrowEachState.Length &&
            _currentGrowDay < _seed.DayToGrowEachState[_currentStage]) return;
        _currentStage++;
        _currentGrowDay = 0;
        srPlant.sprite = _seed.SpritePlantGrows[_currentStage];
    }

    public void Harvest()
    {
        Pool.Release(this);
    }
}