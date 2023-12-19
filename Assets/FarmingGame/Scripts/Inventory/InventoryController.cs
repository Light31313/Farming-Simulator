using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryController : MonoBehaviour
{
    private InputMaster.PlayerActions PlayerActions => InputHelper.Input.Player;

    private void Start()
    {
        InventoryData.InitInventory(new List<InventoryItem>
        {
            new(DropItemData.GetInstance().ItemDictionary[ItemType.Axe], 0),
            new(DropItemData.GetInstance().ItemDictionary[ItemType.Hoe], 1),
            new(DropItemData.GetInstance().ItemDictionary[ItemType.WateringPot], 2)
        });
        
        PlayerActions.Inventory0.performed += _ => OnChangeItemHold(9);
        PlayerActions.Inventory1.performed += _ => OnChangeItemHold(0);
        PlayerActions.Inventory2.performed += _ => OnChangeItemHold(1);
        PlayerActions.Inventory3.performed += _ => OnChangeItemHold(2);
        PlayerActions.Inventory4.performed += _ => OnChangeItemHold(3);
        PlayerActions.Inventory5.performed += _ => OnChangeItemHold(4);
        PlayerActions.Inventory6.performed += _ => OnChangeItemHold(5);
        PlayerActions.Inventory7.performed += _ => OnChangeItemHold(6);
        PlayerActions.Inventory8.performed += _ => OnChangeItemHold(7);
        PlayerActions.Inventory9.performed += _ => OnChangeItemHold(8);
    }
    
    private void OnChangeItemHold(int itemPos)
    {
        if(!isActiveAndEnabled) return;
        InventoryData.ChangeHoldItem(itemPos);
    }
}