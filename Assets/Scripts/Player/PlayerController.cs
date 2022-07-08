using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Zenject;
using WeWillSurvive;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform _cameraRoot;
    [Inject] private WaveSurvivalProject.PlayerActions _playerControls = default;

    [Inject] private WeWillSurvive.PlayerMovement _playerMovement = default;
    [Inject] private PlayerLook _playerLook = default;
    [Inject] private PlayerWeapon _playerWeapon = default;
    [Inject] private PlayerInventory _playerInventory = default;

    
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
