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

    public void UpdateItem(InventoryItem item)
    {
        if (item == null)
        {
            imgItem.gameObject.SetActive(false);
            txtStack.gameObject.SetActive(false);
            return;
        }

        imgItem.gameObject.SetActive(true);
        imgItem.sprite = item.Config.SpriteItem;

        if (item.CurrentStack <= 1)
        {
            txtStack.gameObject.SetActive(false);
        }
        else
        {
            txtStack.gameObject.SetActive(true);
            txtStack.text = $"x{item.CurrentStack}";
        }
    }

    public void ChooseItem()
    {
        tgItem.isOn = true;
    }
}