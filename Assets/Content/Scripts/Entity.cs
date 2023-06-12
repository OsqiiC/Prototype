using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Newtonsoft.Json;

[Serializable]
public class Entity
{
    public Vector3Json rotation = new Vector3Json(Vector3.zero);
    public Vector3Json position = new Vector3Json(Vector3.zero);
    public Health healthComponent;
    public List<Item.ItemData> items = new List<Item.ItemData>();

    [Serializable]
    public class Health
    {
        public float maxHealth;
        public float currentHealth;

        public float CurrentHealthNormalized => currentHealth / maxHealth;

        public void Increase(float value)
        {
            currentHealth += Mathf.Clamp(value, 0, maxHealth - currentHealth);
        }

        public void Decrease(float value)
        {
            currentHealth -= Mathf.Clamp(value, 0, currentHealth);
        }
    }

    [Serializable]
    public class Vector3Json
    {
        public float x;
        public float y;
        public float z;

        public Vector3Json(Vector3 vector3)
        {
            x = vector3.x;
            y = vector3.y;
            z = vector3.z;
        }

        [JsonIgnore]
        public Vector3 Vector3 => new Vector3(x, y, z);
    }
}
