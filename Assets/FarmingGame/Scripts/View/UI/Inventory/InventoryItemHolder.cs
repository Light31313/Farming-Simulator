using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class InventoryItemHolder : MonoBehaviour
{
    [SerializeField] private Image imgItem;
    [SerializeField] private Toggle tgItem;
    [SerializeField] private TextMeshProUGUI txtStack;

    public void InitToggleEvent(UnityAction<bool> tgEvent)
    {
        tgItem.onValueChanged.AddListener(isOn => { tgEvent?.Invoke(isOn); });
    }

    public void UpdateItem(InventoryItemData itemData)
    {
        if (itemData == null)
        {
            imgItem.gameObject.SetActive(false);
            txtStack.gameObject.SetActive(false);
            return;
        }

        imgItem.gameObject.SetActive(true);
        imgItem.sprite = itemData.Config.SpriteItem;

        if (itemData.CurrentStack <= 1)
        {
            txtStack.gameObject.SetActive(false);
        }
        else
        {
            txtStack.gameObject.SetActive(true);
            txtStack.text = $"x{itemData.CurrentStack}";
        }
    }

    public void ChooseItem()
    {
        tgItem.isOn = true;
    }
}