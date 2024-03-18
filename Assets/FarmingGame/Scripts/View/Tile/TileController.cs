using System.Collections.Generic;
using GgAccel;
using GgAccelSDK.Script;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileController : MonoBehaviour
{
    [SerializeField] private Plant plantPrefab;
    [SerializeField] private TileViewModel viewModel;
    [SerializeField] private Tilemap interactableMap, indicatorMap;
    [SerializeField] private Tile hiddenInteractableTile;
    [SerializeField] private AnimatedTile indicatorTile;
    [SerializeField] private AdvancedRuleTile tileDirt;
    [SerializeField] private AudioClip[] sfxHoes;
    [SerializeField] private AudioClip[] sfxSowSeeds;

    public Color WateredTileColor => new(0.8f, 0.8f, 0.8f, 1f);

    private void Start()
    {
        var interactableTiles = new Dictionary<Vector3Int, bool>();

        foreach (var pos in interactableMap.cellBounds.allPositionsWithin)
        {
            var tile = interactableMap.GetTile(pos);
            if (tile)
            {
                interactableMap.SetTile(pos, hiddenInteractableTile);
                interactableTiles[pos] = true;
            }
        }

        viewModel.UpdateInteractableTiles(interactableTiles);
    }

    private void OnEnable()
    {
        TimeSignals.OnStartNewDay.AddListener(OnStartNewDay);
        TileViewModel.OnWateringTile.AddListener(OnWateringTile);
        TileViewModel.OnUpdateIndicator.AddListener(OnUpdateIndicator);
        TileViewModel.OnSowSeedSuccess.AddListener(OnSowSeedSuccess);
        TileViewModel.OnHoeTileSuccess.AddListener(OnHoeTileSuccess);
    }

    private void OnHoeTileSuccess(Vector3Int hoePos)
    {
        interactableMap.SetTile(hoePos, tileDirt);
        interactableMap.SetColor(hoePos, Color.white);
        sfxHoes.PlayRandomClips();
    }

    private void OnSowSeedSuccess(FarmingTileInfo seed, Vector3Int sowPos)
    {
        var plant = Pool.Get(plantPrefab, interactableMap.transform);
        plant.transform.position = new Vector3(sowPos.x + 0.5f, sowPos.y + 0.4f, 0);
        plant.InitPlant(seed);
        sfxSowSeeds.PlayRandomClips();
    }

    private void OnDisable()
    {
        TimeSignals.OnStartNewDay.RemoveListener(OnStartNewDay);
        TileViewModel.OnWateringTile.RemoveListener(OnWateringTile);
        TileViewModel.OnUpdateIndicator.RemoveListener(OnUpdateIndicator);
        TileViewModel.OnSowSeedSuccess.RemoveListener(OnSowSeedSuccess);
        TileViewModel.OnHoeTileSuccess.RemoveListener(OnHoeTileSuccess);
    }

    private void OnWateringTile(Vector3Int waterPos, bool isWatering)
    {
        interactableMap.SetColor(waterPos, isWatering ? WateredTileColor : Color.white);
    }

    private void OnStartNewDay(DayData dayData)
    {
        viewModel.GrowAllPlants();
    }

    private void OnUpdateIndicator(Vector3Int newPos)
    {
        indicatorMap.ClearAllTiles();
        if (newPos == -Vector3Int.one) return;
        indicatorMap.SetTile(newPos, indicatorTile);
    }
}