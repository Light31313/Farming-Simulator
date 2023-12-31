using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemHolder : MonoBehaviour
{
    [SerializeField] private Image imgItem;
    [SerializeField] private Toggle tgItem;
    [SerializeField] private TextMeshProUGUI txtStack;
    public int itemPos;

    public void UpdateItem(Sprite spriteItem, int stack)
    {
        if (!spriteItem)
        {
            imgItem.gameObject.SetActive(false);
        }
        else
        {
            imgItem.gameObject.SetActive(true);
            imgItem.sprite = spriteItem;
        }

        if (stack <= 1)
        {
            txtStack.gameObject.SetActive(false);
        }
        else
        {
            txtStack.gameObject.SetActive(true);
            txtStack.text = $"x{stack}";
        }
    }

    public void ChooseItem()
    {
        tgItem.isOn = true;
    }
}