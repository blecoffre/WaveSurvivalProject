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

        [Inject] private PlayerMovementData _movementData = default;

        private ReactiveProperty<float> _walkSpeed;
        private ReactiveProperty<float> _runSpeed;
        private ReactiveProperty<float> _jumpVelocity;

        private float _currentSpeed = 0.0f;
        Vector3 horizontalVelocity = Vector3.zero;
        private float _gravity = -30.0f;
        private Vector3 _verticalVelocity = Vector3.zero;
        private bool _isGrounded = default;
        public Vector2 Input;
        private bool _jump = false;

        private static int ANIMATOR_PARAM_WALK_SPEED =
            Animator.StringToHash("walkSpeed");

        [Inject]
        private void Init(PlayerMovementData movementData)
        {
            _movementData = movementData;

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

            //horizontalVelocity = (transform.right * Input.x + transform.forward * Input.y) * _currentSpeed;

            if (_jump && _isGrounded)
            {
                _verticalVelocity.y = Mathf.Sqrt(-2f * _jumpVelocity.Value * _gravity);
                _jump = false;
            }

            //_verticalVelocity.y += _gravity * Time.deltaTime;

            //Debug.Log(horizontalVelocity.magnitude);
            //if(horizontalVelocity.magnitude > 0)
            //{
            //    horizontalVelocity.Normalize();
            //    horizontalVelocity *= _currentSpeed * Time.deltaTime;
            //    transform.Translate(horizontalVelocity, Space.World);
            //}

            //float velocityZ = Vector3.Dot(horizontalVelocity.normalized, transform.forward);
            //float velocityX = Vector3.Dot(horizontalVelocity.normalized, transform.right);
            //_animator.SetFloat("VelocityZ", velocityZ, 0.1f, Time.deltaTime);
            //_animator.SetFloat("VelocityX", velocityX, 0.1f, Time.deltaTime);

            _animator.SetFloat("VelocityZ", Input.y, 0.1f, Time.deltaTime);
            _animator.SetFloat("VelocityX", Input.x, 0.1f, Time.deltaTime);
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
}
