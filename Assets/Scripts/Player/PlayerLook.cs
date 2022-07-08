using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using Zenject;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] private Rig _aimLayer = default;
    [SerializeField] private Transform _cameraRoot = default;

    private ReactiveProperty<float> _sensitivityX;
    private ReactiveProperty<float> _sensitivityY;
    public Vector2 Mouse;

    private Camera _playerCamera = default;
    private float _xClamp = 85f;
    private float _xRotation = 0;

    private bool _lockMovement = false;

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

        transform.position = _cameraRoot.position;
        _aimLayer.weight = 1.0f;
    }

    [Inject]
    private void Init(PlayerLookData data, Camera camera)
    {
        _sensitivityX = data.GetXSensitivity();
        _sensitivityY = data.GetYSensitivity();

        _playerCamera = camera;
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
        Mouse.x *= _sensitivityX.Value;
        Mouse.y *= _sensitivityY.Value;

        _xRotation -= Mouse.y * _sensitivityX.Value * Time.deltaTime;
        _xRotation = Mathf.Clamp(_xRotation, -_xClamp, _xClamp);

        _playerCamera.transform.localRotation = Quaternion.Euler(_xRotation, 0, 0);
        transform.Rotate(Vector3.up, Mouse.x * _sensitivityX.Value * Time.deltaTime);
    }
}
