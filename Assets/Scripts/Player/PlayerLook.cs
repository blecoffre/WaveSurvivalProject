using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using Zenject;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] private Rig _aimLayer = default;

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

        _aimLayer.weight = 1.0f;
    }

    [Inject]
    private void Init(PlayerLookData data, Camera camera)
    {
        _sensitivityX = data.GetXSensitivity();
        _sensitivityY = data.GetYSensitivity();

        _playerCamera = camera;
    }

    private void Update()
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

        transform.Rotate(transform.up, Mouse.x * Time.deltaTime);

        _xRotation -= Mouse.y;
        _xRotation = Mathf.Clamp(_xRotation, -_xClamp, _xClamp);
        Vector3 targetRotation = transform.eulerAngles;
        targetRotation.x = _xRotation;
        _playerCamera.transform.eulerAngles = targetRotation;
    }
}


//using System.Collections;
//using System.Collections.Generic;
//using UniRx;
//using UnityEngine;
//using Zenject;

//public class PlayerLook : MonoBehaviour
//{
//    [SerializeField] private float _turnSpeed = 15.0f;
//    [SerializeField] private float _headSpeed = 5.0f;

//    private ReactiveProperty<float> _sensitivityX;
//    private ReactiveProperty<float> _sensitivityY;
//    public Vector2 Mouse;

//    private Camera _playerCamera = default;
//    private float _xClamp = 85f;
//    private float _xRotation = 0;

//    private bool _lockMovement = false;

//    #region Debug Var
//#if UNITY_EDITOR
//    [Inject] private WaveSurvivalProject.DebugActions _debugActions = default;
//#endif
//    #endregion

//    private void Start()
//    {
//#if UNITY_EDITOR
//        _debugActions.OpenDebug.performed += _ => _lockMovement = !_lockMovement;
//#endif
//    }

//    [Inject]
//    private void Init(PlayerLookData data, Camera camera)
//    {
//        _sensitivityX = data.GetXSensitivity();
//        _sensitivityY = data.GetYSensitivity();

//        _playerCamera = camera;
//    }

//    private void Update()
//    {
//        if (!_lockMovement)
//        {
//            Look();
//        }
//    }

//    private void Look()
//    {
//        Mouse.x *= _sensitivityX.Value;
//        Mouse.y *= _sensitivityY.Value;

//        //transform.Rotate(Vector3.up, Mouse.x * Time.deltaTime);
//        //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, transform.rotation.y + Mouse.y, 0), _turnSpeed * Time.deltaTime);
//        //Debug.Log("Mouse" + Mouse.x);
//        //Debug.Log("Rot" + transform.rotation);
//        //_xRotation -= Mouse.y;
//        //_xRotation = Mathf.Clamp(_xRotation, -_xClamp, _xClamp);
//        //Vector3 targetRotation = transform.eulerAngles;
//        //targetRotation.x = _xRotation;
//        //_playerCamera.transform.rotation = Quaternion.Slerp(_playerCamera.transform.rotation, Quaternion.Euler(targetRotation.x, 0, 0), _headSpeed * Time.deltaTime);
//        //_playerCamera.transform.eulerAngles = targetRotation;

//        //transform.Rotate(transform.up, Mouse.x);
//    }
//}
