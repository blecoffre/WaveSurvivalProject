using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class PlayerMovement : MonoBehaviour
{
    [Inject] private CharacterController _characterController = default;

    private float _speed = 11f;
    private float _gravity = -30.0f;
    private Vector3 _verticalVelocity = Vector3.zero;
    [SerializeField] private Transform _feet = default;
    [SerializeField] private LayerMask _groundMask = default;
    private bool _isGrounded = default;
    public Vector2 HorizontalInput;
    private bool _jump = false;
    private float _jumpHeight = 3.5f;

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        _isGrounded = Physics.CheckSphere(_feet.position, 0.1f, _groundMask);
        if (_isGrounded)
        {
            _verticalVelocity.y = 0;
        }

        Vector3 horizontalVelocity = (transform.right * HorizontalInput.x + transform.forward * HorizontalInput.y) * _speed;
        _characterController.Move(horizontalVelocity * Time.deltaTime);

        if (_jump && _isGrounded)
        {
            _verticalVelocity.y = Mathf.Sqrt(-2f * _jumpHeight * _gravity);
            _jump = false;
        }

        _verticalVelocity.y += _gravity * Time.deltaTime;
        _characterController.Move(_verticalVelocity * Time.deltaTime);
    }

    public void OnJumpPressed()
    {
        _jump = true;
    }
}
