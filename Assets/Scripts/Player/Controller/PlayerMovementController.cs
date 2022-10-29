using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace WeWillSurvive
{
    public class PlayerMovementController : MonoBehaviour
    {
        [SerializeField] private Transform _rootTransform = default;
        [SerializeField] private Transform _feet = default;
        [SerializeField] private LayerMask _groundMask = default;
        [SerializeField] private Animator _animator = default;
        [SerializeField] private Rigidbody _rigidbody = default;
        [SerializeField] private Camera _camera = default;

        [Inject] private PlayerAnimatorController _animatorController = default;

        private ReactiveProperty<float> _walkSpeed;
        private ReactiveProperty<float> _runSpeed;
        private ReactiveProperty<float> _jumpForce;
        private ReactiveProperty<float> _animationBlendSpeed;
        private ReactiveProperty<float> _groundDrag;
        private ReactiveProperty<float> _airMultiplier;
        private ReactiveProperty<float> _rotationSpeed;

        private float _currentSpeed = 0.0f;
        private float _moveAmount = 0.0f;
        private ReactiveProperty<bool> _isGrounded = new ReactiveProperty<bool>(false);
        public Vector2 Input;
        private bool _isJumping = false;

        private Vector3 _moveDirection;

        [Inject]
        private void Init(PlayerMovementData movementData)
        {
            _walkSpeed = movementData.GetWalkSpeed();
            _runSpeed = movementData.GetRunSpeed();
            _jumpForce = movementData.GetJumpVelocity();
            _animationBlendSpeed = movementData.GetAnimationBlendSpeed();
            _groundDrag = movementData.GetGroundDrag();
            _airMultiplier = movementData.GetAirMultiplier();
            _rotationSpeed = movementData.GetRotationSpeed();

            _currentSpeed = _walkSpeed.Value;


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

            _moveDirection = (_camera.transform.forward * Input.y) + (_camera.transform.right * Input.x);
            _moveDirection.Normalize();
            _moveDirection.y = 0;
            _moveDirection *= _currentSpeed;

            Vector3 movementVelocity = _moveDirection;
            _rigidbody.velocity = movementVelocity;

            Vector3 targetDirection = Vector3.zero;
            targetDirection = (_camera.transform.forward * Input.y) + (_camera.transform.right * Input.x);
            targetDirection.Normalize();
            targetDirection.y = 0;

            if (targetDirection == Vector3.zero)
            {
                targetDirection = _rootTransform.forward;
            }

            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotationSpeed.Value * Time.deltaTime);
            _rootTransform.rotation = playerRotation;

            //if (_isGrounded.Value)
            //{
            //    _animator.SetFloat("InputY", Input.y, 0.1f, Time.deltaTime);
            //    _animator.SetFloat("InputX", Input.x, 0.1f, Time.deltaTime);
            //}
            //else if (!_isGrounded.Value)
            //{
            //    //_rigidbody.AddForce(_moveDirection.normalized * (_currentSpeed * _airMultiplier.Value), ForceMode.Force);
            //}  

            _moveAmount = Mathf.Clamp01(Mathf.Abs(Input.x) + Mathf.Abs(Input.y));
            _animatorController.UpdateAnimatorMovementValues(0, _moveAmount);
        }

        //private void SpeedControl()
        //{
        //    Vector3 flatVel = new Vector3(_rigidbody.velocity.x, 0f, _rigidbody.velocity.z);
        //    Debug.Log("FlatVel " + flatVel.magnitude);

        //    // limit velocity if needed
        //    if (flatVel.magnitude > _currentSpeed)
        //    {
        //        Debug.Log("TooFast");
        //        Vector3 limitedVel = flatVel.normalized * _currentSpeed;
        //        _rigidbody.velocity = new Vector3(limitedVel.x, _rigidbody.velocity.y, limitedVel.z);
        //    }
        //}

        public void OnJumpPressed()
        {
            if(_isJumping == false)
            {
                _isJumping = true;
                _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, 0f, _rigidbody.velocity.z);

                _rigidbody.AddForce(_rootTransform.up * _jumpForce.Value, ForceMode.Impulse);
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
