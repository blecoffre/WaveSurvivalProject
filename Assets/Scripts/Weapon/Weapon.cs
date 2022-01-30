using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Weapon : MonoBehaviour, IAttack
{
    [SerializeField] protected Camera _camera = default;
    [SerializeField] protected WeaponBaseData _data = default;

    public virtual void Attack()
    {
        RaycastHit hit;
        if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, out hit, _data.GetRange().Value))
        {
            IDamageable damageable = hit.transform.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(_data.GetDamage().Value);
            }
        }
    }
}
