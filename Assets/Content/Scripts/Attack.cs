using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Attack : MonoBehaviour
{
    public float lifeTime;
    public float damage;
    public Rigidbody2D rigidbody;

    public Character ignoredCharacter;

    private void Awake()
    {
        StartCoroutine(DestroyAfterTime());
    }

    protected Attack Create<T>(T ignoredCharacter, Transform parent, Vector3 position, Quaternion rotation) where T : Character
    {
        var attack = Instantiate(original: this, parent: parent, position: position, rotation: rotation);
        attack.ignoredCharacter = ignoredCharacter;

        return attack;
    }

    protected virtual void DestroyProjectile()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Obstacle>())
        {
            DestroyProjectile();
        }

        if (collision.TryGetComponent(out Character character))
        {
            if (character.GetType() == ignoredCharacter.GetType()) return;
            character.TakeDamage(damage);
            DestroyProjectile();
        }
    }

    private IEnumerator DestroyAfterTime()
    {
        yield return new WaitForSeconds(lifeTime);
        DestroyProjectile();
    }
}
