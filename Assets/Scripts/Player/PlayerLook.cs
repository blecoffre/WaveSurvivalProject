using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Zenject;

public class PlayerLook : MonoBehaviour
{
    private ReactiveProperty<float> _sensitivityX;
    private ReactiveProperty<float> _sensitivityY;
    public Vector2 Mouse;

    private Camera _playerCamera = default;
    private float _xClamp = 85f;
    private float _xRotation = 0;

    [Inject]
    private void Init(PlayerLookData data, Camera camera)
    {
        _sensitivityX = data.GetXSensitivity();
        _sensitivityY = data.GetYSensitivity();

        _playerCamera = camera;
    }

    private void Update()
    {
        Look();
    }

    private void Look()
    {
        Mouse.x *= _sensitivityX.Value;
        Mouse.y *= _sensitivityY.Value;

        transform.Rotate(Vector3.up, Mouse.x * Time.deltaTime);

        _xRotation -= Mouse.y;
        _xRotation = Mathf.Clamp(_xRotation, -_xClamp, _xClamp);
        Vector3 targetRotation = transform.eulerAngles;
        targetRotation.x = _xRotation;
        _playerCamera.transform.eulerAngles = targetRotation;
    }
}
