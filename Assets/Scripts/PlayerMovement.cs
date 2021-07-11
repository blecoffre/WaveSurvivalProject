using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 1.0f;
    [SerializeField] private float gravity = -9.81f;

    public Transform _groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask _groundMask;

    Vector3 velocity;

    private bool _isGrounded = false;

    private void Start()
    {
        
    }

    private void Update()
    {
        _isGrounded = Physics.CheckSphere(_groundCheck.position, groundDistance, _groundMask);

        if (_isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
    }
}
