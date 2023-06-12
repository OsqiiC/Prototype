using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ItemCard : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private Button deleteButton;
    [SerializeField] private TMPro.TMP_Text quantityText;

    public UnityEvent onDeletePressed => deleteButton.onClick;

    private Item.ItemData itemData;

    public Image Image => image;

    public void SetActiveDeleteButton(bool value) => deleteButton.gameObject.SetActive(value);

    public int Quantity
    {
        get => itemData .currentQuantity;
        set
        {
            itemData.currentQuantity = value;
            quantityText.gameObject.SetActive(itemData.currentQuantity > 1);
            quantityText.text = itemData.currentQuantity.ToString();
        }
    }

    public Item.ItemData ItemData
    {
        get
        {
            return new Item.ItemData
            {
                itemID = itemData.itemID,
                currentQuantity = itemData.currentQuantity,
                maxQuantity = itemData.maxQuantity
            };
        }
        set
        {
            itemData = new Item.ItemData
            {
                itemID = value.itemID,
                currentQuantity = value.currentQuantity,
                maxQuantity = value.maxQuantity
            };
        }
    }
}
