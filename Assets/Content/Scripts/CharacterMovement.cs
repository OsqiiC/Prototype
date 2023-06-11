using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private float _speed;

    private Vector2 _movementDirection = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Move();
    }

    public void Move(Vector2 inputDirection)
    {
        Debug.Log(inputDirection);
        _movementDirection = inputDirection.normalized;
    }

    private void Move()
    {
        _rigidbody.velocity = _movementDirection * _speed;
    }
}
