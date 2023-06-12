using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField] private Button openButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private GridLayoutGroup itemsGrid;
    [SerializeField] private Toggle deleteItemButton;
    [SerializeField] private ItemCard itemCardPrefab;

    public UnityEvent<int> onItemRemoved = new UnityEvent<int>();

    private List<ItemCard> itemCards = new List<ItemCard>();

    public void Initalize()
    {
        openButton.onClick.AddListener(Open);
        closeButton.onClick.AddListener(Close);
        deleteItemButton.onValueChanged.AddListener(value =>
        {
            if (value) Open();
            foreach (var item in itemCards)
            {
                item.SetActiveDeleteButton(value);
            }
        });
        Close();
    }

    private void Open()
    {
        gameObject.SetActive(true);
    }

    private void Close()
    {
        gameObject.SetActive(false);
        deleteItemButton.isOn = false;
    }

    public void AddItem(Item.ItemData itemData)
    {
        if (TryGetItem(itemData.itemID, out ItemCard itemCard))
        {
            itemCard.Quantity++;
        }
        else
        {
            AddCardInternal(ItemPrefabStorage.Instance.GetItemPrefab(itemData.itemID));
        }
    }

    public void AddItem(Item item)
    {
        if (TryGetItem(item.itemData.itemID, out ItemCard itemCard))
        {
            itemCard.Quantity++;
        }
        else
        {
            AddCardInternal(item);
        }
    }

    public void Clear()
    {
        foreach (var item in itemCards)
        {
            Destroy(item.gameObject);
        }

        itemCards.Clear();
    }

    private bool TryGetItem(int id, out ItemCard itemCard)
    {
        itemCard = null;

        foreach (var item in itemCards)
        {
            if (item.ItemData.itemID == id && item.Quantity < item.ItemData.maxQuantity)
            {
                itemCard = item;
                return true;
            }
        }

        return false;
    }

    private void AddCardInternal(Item item)
    {
        var card = Instantiate(itemCardPrefab, itemsGrid.transform);
        card.SetActiveDeleteButton(deleteItemButton.isOn);
        card.ItemData = item.itemData;
        card.Image.sprite = item.spriteRenderer.sprite;
        card.Quantity = 1;
        card.onDeletePressed.AddListener(() => RemoveItem(card));
        itemCards.Add(card);
    }

    private void RemoveItem(ItemCard itemCard)
    {
        if (itemCard.Quantity > 1)
        {
            itemCard.Quantity--;
        }
        else
        {
            itemCards.Remove(itemCard);
            Destroy(itemCard.gameObject);
        }

        onItemRemoved?.Invoke(itemCard.ItemData.itemID);
    }
}
