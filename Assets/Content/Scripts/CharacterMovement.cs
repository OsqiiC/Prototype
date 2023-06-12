using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigidbody;
    [SerializeField] private float speed;

    private Vector2 movementDirection = Vector2.zero;

    private void FixedUpdate()
    {
        Move();
    }

    public void Move(Vector2 inputDirection)
    {
        movementDirection = inputDirection.normalized;
    }

    private void Move()
    {
        rigidbody.velocity = movementDirection * speed;
    }
}
