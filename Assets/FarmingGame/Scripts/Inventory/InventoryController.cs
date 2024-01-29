using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    private InputMaster.PlayerActions PlayerActions => InputHelper.Input.Player;

    private void Awake()
    {
        InventoryData.InitInventory(new List<InventoryItem>
        {
            new(ItemData.GetInstance().ItemDictionary[ItemType.Axe], 0),
            new(ItemData.GetInstance().ItemDictionary[ItemType.Hoe], 1),
            new(ItemData.GetInstance().ItemDictionary[ItemType.WateringPot], 2),
            new(ItemData.GetInstance().ItemDictionary[ItemType.CabbageSeed], 3, 10),
            new(ItemData.GetInstance().ItemDictionary[ItemType.CarrotSeed], 4, 10),
        });
    }

    private void Start()
    {
        PlayerActions.Inventory1.performed += _ => OnChangeItemHold(0);
        PlayerActions.Inventory2.performed += _ => OnChangeItemHold(1);
        PlayerActions.Inventory3.performed += _ => OnChangeItemHold(2);
        PlayerActions.Inventory4.performed += _ => OnChangeItemHold(3);
        PlayerActions.Inventory5.performed += _ => OnChangeItemHold(4);
        PlayerActions.Inventory6.performed += _ => OnChangeItemHold(5);
        PlayerActions.Inventory7.performed += _ => OnChangeItemHold(6);
        PlayerActions.Inventory8.performed += _ => OnChangeItemHold(7);
        GamePlayModel.Instance.isInitDataDone = true;
    }

    private void OnChangeItemHold(int itemPos)
    {
        if (!isActiveAndEnabled) return;
        InputSignals.OnQuickAccessInventoryClick.Dispatch(itemPos);
    }
}