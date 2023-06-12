using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPrefabStorage : MonoBehaviour
{
    public static ItemPrefabStorage Instance { get; private set; }

    [SerializeField] private List<Item> itemPrefabs;

    public void Initalize()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
        }
    }

    public Item GetItemPrefab(int id)
    {
        foreach (var item in itemPrefabs)
        {
            if (item.itemData.itemID == id)
            {
                return item;
            }
        }

        return null;
    }

    public Item.ItemData GetRandomItemData()
    {
        return itemPrefabs[Random.Range(0, itemPrefabs.Count)].itemData;
    }
}
