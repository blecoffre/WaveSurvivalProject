using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerLook : MonoBehaviour
{
    private float _sensitivityX = 80f;
    private float _sensitivityY = 0.5f;
    public Vector2 Mouse;
    

    [Inject] private Camera _playerCamera = default;
    private float _xClamp = 85f;
    private float _xRotation = 0;

    private void Update()
    {
        Look();
    }

    private void Look()
    {
        Mouse.x *= _sensitivityX;
        Mouse.y *= _sensitivityY;

        transform.Rotate(Vector3.up, Mouse.x * Time.deltaTime);

        _xRotation -= Mouse.y;
        _xRotation = Mathf.Clamp(_xRotation, -_xClamp, _xClamp);
        Vector3 targetRotation = transform.eulerAngles;
        targetRotation.x = _xRotation;
        _playerCamera.transform.eulerAngles = targetRotation;
    }
}
