using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ItemCollector : MonoBehaviour
{
    public UnityEvent<Item> onItemObtained = new UnityEvent<Item>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out Item item))
        {
            onItemObtained?.Invoke(item);
            Destroy(item.gameObject);
        }
    }
}
