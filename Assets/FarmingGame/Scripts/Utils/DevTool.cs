using Sirenix.OdinInspector;
using UnityEngine;

public class DevTool : MonoBehaviour
{
    [SerializeField] private TileViewModel tileViewModel;
    [SerializeField] private ItemData itemData;

    [Button]
    public void GoToSleep()
    {
        TimeSignals.OnGoToSleep.Dispatch();
    }

    [Button]
    public void GrowAllPlantsImmediately()
    {
        tileViewModel.GrowAllPlantsImmediately();
    }

    [Button]
    public void InitHarvestablePlants()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                tileViewModel.UpdateIndicatorPos(new Vector3Int(i, j, 0));
                tileViewModel.HoeTile();
                tileViewModel.SowSeed(itemData.ItemDictionary[ItemType.BeetSeed]);
            }
        }

        tileViewModel.GrowAllPlantsImmediately();
    }
}