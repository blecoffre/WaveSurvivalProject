using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace WeWillSurvive
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private Transform _feet = default;
        [SerializeField] private LayerMask _groundMask = default;
        [SerializeField] private Animator _animator = default;
        [SerializeField] private Rigidbody _rigidbody = default;
        [SerializeField] private Camera _camera = default;

        [Inject] private PlayerMovementData _movementData = default;

        private ReactiveProperty<float> _walkSpeed;
        private ReactiveProperty<float> _runSpeed;
        private ReactiveProperty<float> _jumpForce;
        private ReactiveProperty<float> _animationBlendSpeed;
        private ReactiveProperty<float> _groundDrag;
        private ReactiveProperty<float> _airMultiplier;

        private float _currentSpeed = 0.0f;
        private int _xVelHash;
        private int _yVelHash;
        private Vector2 _horizontalVelocity;
        private float _gravity = -30.0f;
        private ReactiveProperty<bool> _isGrounded = new ReactiveProperty<bool>(false);
        public Vector2 Input;
        private bool _isJumping = false;

        private Vector3 _moveDirection;

        [Inject]
        private void Init(PlayerMovementData movementData)
        {
            _movementData = movementData;

            _walkSpeed = _movementData.GetWalkSpeed();
            _runSpeed = _movementData.GetRunSpeed();
            _jumpForce = _movementData.GetJumpVelocity();
            _animationBlendSpeed = _movementData.GetAnimationBlendSpeed();
            _groundDrag = _movementData.GetGroundDrag();
            _airMultiplier = _movementData.GetAirMultiplier();

            _currentSpeed = _walkSpeed.Value;

            _xVelHash = Animator.StringToHash("InputX");
            _yVelHash = Animator.StringToHash("InputY");

            Observable.EveryFixedUpdate().Subscribe(_ => Move());
            //Observable.EveryUpdate().Subscribe(_ => SpeedControl());

            _isGrounded.Subscribe(x =>
            {
                if(x is true)
                {
                    _isJumping = false;
                }
            });
        }

        private void Move()
        {
            if (_animator == null) return;

            _isGrounded.Value = Physics.CheckSphere(_feet.position, 0.1f, _groundMask);

            if (_isGrounded.Value)
            {
                _rigidbody.drag = _groundDrag.Value;
            }
            else
            {
                _rigidbody.drag = 0;
            }

            _moveDirection = transform.forward * Input.y + transform.right * Input.x;

            var truc = _moveDirection.normalized * _currentSpeed;
            Debug.Log(_moveDirection.normalized * _currentSpeed);

            _horizontalVelocity.x = Mathf.Lerp(_horizontalVelocity.x, Input.x * _currentSpeed, _animationBlendSpeed.Value * Time.fixedDeltaTime);
            _horizontalVelocity.y = Mathf.Lerp(_horizontalVelocity.y, Input.y * _currentSpeed, _animationBlendSpeed.Value * Time.fixedDeltaTime);

            //var xDifference = _horizontalVelocity.x - _rigidbody.velocity.x;
            //var zDifference = _horizontalVelocity.y - _rigidbody.velocity.z;

            if (_isGrounded.Value)
            {
                _rigidbody.AddForce(_moveDirection.normalized * _currentSpeed, ForceMode.Force);
            }
            else if (!_isGrounded.Value)
            {
                _rigidbody.AddForce(_moveDirection.normalized * _currentSpeed * _airMultiplier.Value, ForceMode.Force);
            }

            //_animator.SetFloat(_xVelHash, _horizontalVelocity.x);
            //_animator.SetFloat(_yVelHash, _horizontalVelocity.y);

            _animator.SetFloat("InputY", Input.y, 0.1f, Time.deltaTime);
            _animator.SetFloat("InputX", Input.x, 0.1f, Time.deltaTime);
        }

        private void SpeedControl()
        {
            Vector3 flatVel = new Vector3(_rigidbody.velocity.x, 0f, _rigidbody.velocity.z);
            Debug.Log("FlatVel " + flatVel.magnitude);

            // limit velocity if needed
            if (flatVel.magnitude > _currentSpeed)
            {
                Debug.Log("TooFast");
                Vector3 limitedVel = flatVel.normalized * _currentSpeed;
                _rigidbody.velocity = new Vector3(limitedVel.x, _rigidbody.velocity.y, limitedVel.z);
            }
        }

        public void OnJumpPressed()
        {
            if(_isJumping == false)
            {
                _isJumping = true;
                _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, 0f, _rigidbody.velocity.z);

                _rigidbody.AddForce(transform.up * _jumpForce.Value, ForceMode.Impulse);
            }
        }

        public void Sprint()
        {
            if (_isGrounded.Value)
            {
                _currentSpeed = _runSpeed.Value;
            }
        }

        public void Walk()
        {
            _currentSpeed = _walkSpeed.Value;
        }
    }
}
