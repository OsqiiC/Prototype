using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : Attack
{
    public Attack Create<T>(T ignoredCharacter, Transform parent) where T : Character => 
        Create(ignoredCharacter, parent, parent.position, Quaternion.identity);
}
