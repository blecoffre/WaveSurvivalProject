using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class PlayerCollecter : MonoBehaviour
{
    private ReactiveProperty<Collectable> _collectable = default;

    private void Start()
    {
        _collectable = new ReactiveProperty<Collectable>();
        _collectable.ObserveEveryValueChanged(x => x.Value).Subscribe(_ => ProcessCollect()).AddTo(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
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
        if (_collectable.Value != null)
        {
            _collectable.Value.DestroyCollectable();
            _collectable.Dispose();
            _collectable = null;
        }
    }
}
