using Cysharp.Threading.Tasks;
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

    #region Debug
    [Inject] protected DebugController _debug;
    private bool _debugOpened = false;
    #endregion

    protected virtual void Start()
    {
#if UNITY_EDITOR
        _debug.WindowIsVisible.Subscribe(x => _debugOpened = x);
#endif
    }

#if UNITY_EDITOR
    private void DebugOpenedState(bool isDebugOpen)
    {
        _debugOpened = isDebugOpen;
    }
#endif

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
}
