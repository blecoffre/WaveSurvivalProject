using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Zenject;
using WeWillSurvive;

public class PlayerController : MonoBehaviour
{
    [Inject] private WaveSurvivalProject.PlayerActions _playerControls = default;

    [Inject] private PlayerMovementController _playerMovement = default;
    [Inject] private PlayerLookController _playerLook = default;
    //[Inject] private PlayerLook _playerLook = default;
    [Inject] private PlayerWeaponController _playerWeapon = default;
    [Inject] private PlayerInventoryController _playerInventory = default;
    [Inject] private PlayerInteractionController _playerIneraction = default;
    
    private void Start()
    {
        BindControls();
        BindEvents();
    }

    private void BindControls()
    {
        _playerControls.Move.performed += ctx => _playerMovement.Input = ctx.ReadValue<Vector2>();
        _playerControls.Jump.performed += _ => _playerMovement.OnJumpPressed();
        _playerControls.Look.performed += ctx => _playerLook.Mouse = ctx.ReadValue<Vector2>();
        _playerControls.Fire.performed += _ => _playerWeapon.Attack();
        _playerControls.Fire.canceled += _ => _playerWeapon.StopAttack();
        _playerControls.Sprint.performed += _ => _playerMovement.Sprint();
        _playerControls.Sprint.canceled += _ => _playerMovement.Walk();
        _playerControls.Reload.performed += _ => _playerWeapon.Reload();
        _playerControls.HolstWeapon.performed += _ => _playerWeapon.Holst();
        _playerControls.Aim.performed += _ => _playerLook.StartAim();
        _playerControls.Aim.canceled += _ => _playerLook.StopAim();
        _playerControls.Interact.performed += _ => _playerIneraction.Interact();
    }

    private void BindEvents()
    {
        _playerInventory.CurrentSelectedWeapon.Subscribe(x => _playerWeapon.SetCurrentWeapon(x)).AddTo(_playerInventory);
    }

    private void OnEnable()
    {
        _playerControls.Enable();
    }

    private void OnDisable()
    {
        _playerControls.Disable();
    }
}
