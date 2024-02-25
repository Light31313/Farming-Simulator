using UnityEngine;

public class InventoryController : MonoBehaviour
{
    private InputMaster.PlayerActions PlayerActions => InputHelper.Input.Player;
    [SerializeField] private InventoryViewModel viewModel;

    private void Awake()
    {
        viewModel.InitInventoryData();
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
        GameManager.Instance.isInitDataDone = true;
    }

    private void OnChangeItemHold(int itemPos)
    {
        if (!isActiveAndEnabled) return;
        InputSignals.OnQuickAccessInventoryClick.Dispatch(itemPos);
    }
}