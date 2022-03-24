using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Collectable : MonoBehaviour, ICollectable
{
    private Rigidbody _rigidbody = default;
    private Collider _collider = default;
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.isKinematic = false;

        _collider = GetComponent<Collider>();
        _collider.isTrigger = true;
    }

    public Collectable Collect()
    {
        Debug.Log("Collect");
        return this;
    }

    public void DestroyCollectable()
    {
        Destroy(gameObject);
    }
}
