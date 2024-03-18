using GgAccel;
using UnityEngine;

public class Plant : MonoBehaviour
{
    [SerializeField] private SpriteRenderer srPlant;
    [SerializeField] private TileViewModel tileViewModel;
    private FarmingTileInfo _seed;
    private Transform _cacheTransform;

    private void Start()
    {
        _cacheTransform = transform;
    }

    public void InitPlant(FarmingTileInfo seed)
    {
        _seed = seed;
        srPlant.sprite = seed.Config.SpritePlantGrows[0];
        _seed.CurrentStage.OnChangedValues += OnPlantStageChange;
        _seed.IsHarvested.OnChangedValues += OnHarvested;
    }

    private void OnMouseEnter()
    {
        if (_seed != null && _seed.IsHarvestable())
        {
            Debug.Log("OnMouseEnter");
            MouseController.Instance.ShowInteractableCursor(gameObject);
        }
    }

    private void OnMouseExit()
    {
        Debug.Log("OnMouseExit");
        MouseController.Instance.ShowDefaultCursor();
    }

    private void OnHarvested(bool arg1, bool isHarvested)
    {
        if (isHarvested)
        {
            GameUtils.Instance.DropItem(_cacheTransform.position, _seed.Config.HarvestedPlant, _seed.HarvestQuantity);
            _seed = null;
            Pool.Release(this);
        }
    }

    private void OnPlantStageChange(int oldValue, int newValue)
    {
        srPlant.sprite = _seed.Config.SpritePlantGrows[newValue];
    }

    public void Harvest()
    {
        var plantPos = _cacheTransform.position;
        tileViewModel.Harvest(new Vector3Int((int)plantPos.x, (int)plantPos.y, (int)plantPos.z));
    }
}