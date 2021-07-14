using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Transform _camera = default;
    [SerializeField] private float _range = 50f;
    [SerializeField] private float _damage = 10f;

    [Inject] private PlayerController _controller = default;
    
    public void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(_camera.position, _camera.forward, out hit, _range))
        {
            print(hit.collider.name);
        }
    }
}
