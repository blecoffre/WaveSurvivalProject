using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using Zenject;
using Cinemachine;

public class PlayerLookController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _aimVirtualCamera = default;
    [SerializeField] private Rig _aimLayer = default;
    [SerializeField] private Transform _cameraRoot = default;

    [Tooltip("How far in degrees can you move the camera up")]
    public float TopClamp = 70.0f;
    [Tooltip("How far in degrees can you move the camera down")]
    public float BottomClamp = -30.0f;
    [Tooltip("Additional degress to override the camera. Useful for fine tuning camera position when locked")]
    public float CameraAngleOverride = 0.0f;

    private ReactiveProperty<float> _sensitivityX;
    private ReactiveProperty<float> _sensitivityY;

    public Vector2 Mouse;

    private Camera _playerCamera = default;
    private float _xClamp = 85f;
    private float _xRotation = 0;

    private bool _lockMovement = false;

    private float _cinemachineTargetYaw;
    private float _cinemachineTargetPitch;
    private const float _threshold = 0.01f;

    [Inject] private ShopsManager _shopsManager = default;


    #region Debug Var
#if UNITY_EDITOR
    [Inject] private WaveSurvivalProject.DebugActions _debugActions = default;
#endif
    #endregion

    private void Start()
    {
#if UNITY_EDITOR
        _debugActions.OpenDebug.performed += _ => _lockMovement = !_lockMovement;
#endif

        _shopsManager.OnOpenShop.Subscribe(_ =>
        {
            FreezeCameraMovement();
        }).AddTo(gameObject);

        _shopsManager.OnCloseShop.Subscribe(_ =>
        {
            UnFreezeCameraMovement();
        }).AddTo(gameObject);

        _aimLayer.weight = 1.0f;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        _aimVirtualCamera.gameObject.SetActive(false);
    }

    [Inject]
    private void Init(PlayerLookData data)
    {
        _sensitivityX = data.GetXSensitivity();
        _sensitivityY = data.GetYSensitivity();
    }

    private void LateUpdate()
    {
        if (!_lockMovement)
        {
            Look();
        }
    }

    private void Look()
    {
        // if there is an input and camera position is not fixed
        if (Mouse.sqrMagnitude >= _threshold && !_lockMovement)
        {
            _cinemachineTargetYaw += Mouse.x * Time.deltaTime * _sensitivityX.Value;
            _cinemachineTargetPitch +=Mouse.y * Time.deltaTime * _sensitivityY.Value;
        }

        // clamp our rotations so our values are limited 360 degrees
        _cinemachineTargetYaw = ClampAngle(_cinemachineTargetYaw, float.MinValue, float.MaxValue);
        _cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, BottomClamp, TopClamp);

        // Cinemachine will follow this target
        _aimVirtualCamera.Follow.rotation = Quaternion.Euler(-(_cinemachineTargetPitch + CameraAngleOverride), _cinemachineTargetYaw, 0.0f);
    }

    public void StartAim()
    {
        _aimVirtualCamera.gameObject.SetActive(true);
    }

    public void StopAim()
    {
        _aimVirtualCamera.gameObject.SetActive(false);
    }

    private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
    {
        if (lfAngle < -360f) lfAngle += 360f;
        if (lfAngle > 360f) lfAngle -= 360f;
        return Mathf.Clamp(lfAngle, lfMin, lfMax);
    }

    public void FreezeCameraMovement()
    {
        _lockMovement = true;
    }

    public void UnFreezeCameraMovement()
    {
        _lockMovement = false;
    }
}
