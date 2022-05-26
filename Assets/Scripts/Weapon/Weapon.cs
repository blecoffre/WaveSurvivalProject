using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Zenject;

public class Weapon : MonoBehaviour, IAttack, IWeapon
{
    [SerializeField] protected Camera _camera = default;
    [SerializeField] protected WeaponBaseData _data = default;

    protected ReactiveProperty<bool> _attackPressed = new ReactiveProperty<bool>(false);
    protected IDisposable _attackDisposable = default;

#if UNITY_EDITOR
    #region Debug
    [Inject] protected DebugController _debug;
    private bool _debugOpened = false;
    #endregion
#endif

    protected virtual void Start()
    {
        _camera = Camera.main;
#if UNITY_EDITOR
        _debug.WindowIsVisible.Subscribe(x => _debugOpened = x);
#endif
    }

    public virtual void Attack()
    {
#if UNITY_EDITOR
        if (_debugOpened)
        {
            return;
        }
#endif
        _attackPressed.Value = true;
    }

    public virtual void StopAttack()
    {
        _attackPressed.Value = false;
    }

    //public class WeaponFactory : PlaceholderFactory<GameObject, Weapon>
    //{
    //    public DiContainer _container;

    //    public WeaponFactory(DiContainer container)
    //    {
    //        _container = container;
    //    }

    //    public override Weapon Create()
    //    {
    //        return _container.Instantiate<Weapon>();
    //    }
    //}
}

public class WeaponFactory : PlaceholderFactory<UnityEngine.Object, Weapon>
{

}

public class CustomWeaponFactory : IFactory<IWeapon>
{
    DiContainer _container;
    Weapon _weapon;

    public CustomWeaponFactory(DiContainer container, Weapon weapon)
    {
        _container = container;
        _weapon = weapon;
    }

    public IWeapon Create()
    {
        if(_weapon is RangeWeapon)
        {
            return _container.Instantiate<RangeWeapon>();
        }

        return _container.Instantiate<Weapon>();
    }
}

