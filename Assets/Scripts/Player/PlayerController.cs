using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerController : MonoBehaviour
{
    private WaveSurvivalProject _controls = default;
    private WaveSurvivalProject.PlayerActions _playerControls = default;

    [Inject] private PlayerMovement _playerMovement = default;
    [Inject] private PlayerLook _playerLook = default;
    [Inject] private Weapon _weapon = default;

    

    private void Awake()
    {
        _controls = new WaveSurvivalProject();
        _playerControls = _controls.Player;

        _playerControls.Move.performed += ctx => _playerMovement.HorizontalInput = ctx.ReadValue<Vector2>();
        _playerControls.Jump.performed += _ => _playerMovement.OnJumpPressed();
        _playerControls.Look.performed += ctx => _playerLook.Mouse = ctx.ReadValue<Vector2>();
        _playerControls.Fire.performed += _ => _weapon.Attack();
        _playerControls.Fire.canceled += _ => _weapon.StopAttack();
        _playerControls.Sprint.performed += _ => _playerMovement.Sprint();
        _playerControls.Sprint.canceled += _ => _playerMovement.Walk();
    }

    private void OnEnable()
    {
        _controls.Enable();
    }

    private void OnDisable()
    {
        _controls.Disable();
    }

    private void Update()
    {
        
    }


}
