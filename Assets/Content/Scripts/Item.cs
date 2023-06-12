using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public ItemData itemData;

    [Serializable]
    public class ItemData
    {
        public int maxQuantity;
        public int currentQuantity;
        public int itemID;
    }
}
