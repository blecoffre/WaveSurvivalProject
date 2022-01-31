using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Zenject;

public class Weapon : MonoBehaviour, IAttack
{
    [SerializeField] protected Camera _camera = default;
    [SerializeField] protected WeaponBaseData _data = default;

    private ReactiveProperty<bool> _attackPressed = new ReactiveProperty<bool>(false);
    public virtual void Attack()
    {
        _attackPressed.Value = true;
    }

    public virtual void AttackLoop()
    {

    }

    public virtual void StopAttack()
    {
        _attackPressed.Value = false;
    }
}
