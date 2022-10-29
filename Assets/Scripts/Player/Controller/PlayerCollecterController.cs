using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Zenject;

public class PlayerCollecterController : MonoBehaviour
{
    private ReactiveProperty<Collectable> _collectable = default;

    [Inject] private PlayerInventoryController _playerInventory = default;

    private void Start()
    {
        _collectable = new ReactiveProperty<Collectable>();
        _collectable.ObserveEveryValueChanged(x => x.Value).Subscribe(_ => ProcessCollect()).AddTo(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log(collision.gameObject.name);
    }

    private void OnTriggerEnter(Collider other)
    {
        ICollectable collectable = other.GetComponentInChildren<ICollectable>();

        if (collectable != null)
        {
            _collectable.Value = collectable.Collect();
        }
    }

    private void ProcessCollect()
    {
        switch (_collectable.Value)
        {
            case WeaponPickup wp:
                CollectWeapon(wp.WeaponData);
                break;
        }
        //if (_collectable.Value != null)
        //{
        //    _collectable.Value.DestroyCollectable();
        //    _collectable.Dispose();
        //    _collectable = null;
        //}
    }

    private void CollectWeapon(Weapon _data)
    {
        _playerInventory.CollectedWeapon.Value = _data;
    }
}
