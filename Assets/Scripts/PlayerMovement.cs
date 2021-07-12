using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    //[SerializeField] private float _moveSpeed = 1.0f;
    //[SerializeField] private float gravity = -9.81f;

    //public Transform _groundCheck;
    //public float groundDistance = 0.4f;
    //public LayerMask _groundMask;

    //Vector3 velocity;

    //private bool _isGrounded = false;

    //private void Start()
    //{

    //}

    //private void Update()
    //{
    //    _isGrounded = Physics.CheckSphere(_groundCheck.position, groundDistance, _groundMask);

    //    if (_isGrounded && velocity.y < 0)
    //    {
    //        velocity.y = -2f;
    //    }

    //    float x = Keyboard.current["WASD"]
    //}

    //public void OnMove(InputValue input)
    //{
    //    Vector2 inputVec = input.Get<Vector2>();

    //    moveVec = new Vector3(inputVec.x, 0, inputVec.y);
    //}

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

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(_movementX, 0.0f, _movementY);
        _rb.AddForce(movement * _speed);
    }
}
