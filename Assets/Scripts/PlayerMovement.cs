using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody _rb = default;
    private float _movementX = 0;
    private float _movementY = 0;
    private float _speed = 100;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        _movementX = movementVector.x;
        _movementY = movementVector.y;
    }

    private void OnSpacebar(InputValue value)
    {
        float jumpForce = 1000.0f;
        _rb.AddForce(new Vector3(0, jumpForce, 0));
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(_movementX, 0.0f, _movementY);
        _rb.AddForce(movement * _speed);
    }
}
