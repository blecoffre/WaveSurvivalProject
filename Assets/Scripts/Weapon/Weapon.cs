using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Zenject;

public class Weapon : MonoBehaviour, IAttack
{
    [SerializeField] protected Camera _camera = default;
    [SerializeField] protected WeaponBaseData _data = default;

    protected ReactiveProperty<bool> _attackPressed = new ReactiveProperty<bool>(false);
    protected IDisposable _attackDisposable = default;  

    public virtual void Attack()
    {
            _attackPressed.Value = true;
    }

    public virtual void StopAttack()
    {
        _attackPressed.Value = false;
    }
}
