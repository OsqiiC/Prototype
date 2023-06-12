using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    [SerializeField] protected Entity entity;
    [SerializeField] private Slider healthBar;

    public virtual void TakeDamage(float value)
    {
        entity.healthComponent.Decrease(value);
        RefreshHealtBar();
        if (entity.healthComponent.CurrentHealthNormalized == 0) Death();
    }
    
    public void RefreshHealtBar()
    {
        healthBar.value = entity.healthComponent.CurrentHealthNormalized;
    }

    protected virtual void Death()
    {
        Destroy(gameObject);
    }
}
