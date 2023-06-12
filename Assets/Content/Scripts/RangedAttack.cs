using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttack : Attack
{
    public float speed;

    public Attack Create<T>(T ignoredCharacter, Transform parent, Vector2 direction, Vector3 posiition) where T : Character
    {
        var projectile = Create(ignoredCharacter, parent, posiition, Quaternion.identity);

        projectile.rigidbody.velocity = direction * speed;

        return projectile;
    }
}
