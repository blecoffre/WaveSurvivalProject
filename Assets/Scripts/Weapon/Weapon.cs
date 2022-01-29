using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Weapon : MonoBehaviour, IAttack
{
    [SerializeField] protected Camera _camera = default;
    [SerializeField] protected float _range = 50f;
    [SerializeField] protected float _damage = 10f;
    //[Inject] private IntVariable _startAmmo = default;

    public virtual void Attack()
    {
        RaycastHit hit;
        if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, out hit, _range))
        {
            IDamageable damageable = hit.transform.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(_damage);
            }
        }
    }
}
