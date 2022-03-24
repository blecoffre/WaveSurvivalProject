using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Transform _feet = default;
    [SerializeField] private LayerMask _groundMask = default;
    [SerializeField] private Animator _animator = default;
    [SerializeField] private Rigidbody _rigidbody = default;

    [Inject] private PlayerMovementData _movementData = default;
    [Inject] private CharacterController _characterController = default;

    private ReactiveProperty<float> _walkSpeed;
    private ReactiveProperty<float> _runSpeed;
    private ReactiveProperty<float> _jumpVelocity;

    private float _currentSpeed = 0.0f;
    Vector3 horizontalVelocity = Vector3.zero;
    private float _gravity = -30.0f;
    private Vector3 _verticalVelocity = Vector3.zero;   
    private bool _isGrounded = default;
    public Vector2 HorizontalInput;
    private bool _jump = false;

    private static int ANIMATOR_PARAM_WALK_SPEED =
        Animator.StringToHash("walkSpeed");

    [Inject]
    private void Init(PlayerMovementData movementData, CharacterController characterController)
    {
        _movementData = movementData;
        _characterController = characterController;

        _walkSpeed = _movementData.GetWalkSpeed();
        _runSpeed = _movementData.GetRunSpeed();
        _jumpVelocity = _movementData.GetJumpVelocity();

        _currentSpeed = _walkSpeed.Value;

        Observable.EveryUpdate().Subscribe(_ => Move());
    }

    private void Move()
    {
        _isGrounded = Physics.CheckSphere(_feet.position, 0.1f, _groundMask);
        if (_isGrounded)
        {
            _verticalVelocity.y = 0;
        }

        horizontalVelocity = (transform.right * HorizontalInput.x + transform.forward * HorizontalInput.y) * _currentSpeed;
        _characterController.Move(horizontalVelocity * Time.deltaTime);

        if (_jump && _isGrounded)
        {
            _verticalVelocity.y = Mathf.Sqrt(-2f * _jumpVelocity.Value * _gravity);
            _jump = false;
        }

        _verticalVelocity.y += _gravity * Time.deltaTime;
        _characterController.Move(_verticalVelocity * Time.deltaTime);
    }

    public void OnJumpPressed()
    {
        _jump = true;
    }

    public void Sprint()
    {
        if (_isGrounded)
        {
            _currentSpeed = _runSpeed.Value;
        }
    }

    public void Walk()
    {
        _currentSpeed = _walkSpeed.Value;
    }

    //private void LateUpdate()
    //{
    //    float speed = _rigidbody.velocity.magnitude;
    //    this._animator.SetFloat(ANIMATOR_PARAM_WALK_SPEED, speed);
    //}
}
