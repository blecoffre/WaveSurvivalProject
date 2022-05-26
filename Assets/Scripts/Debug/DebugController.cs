using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

[RequireComponent(typeof(DebugControls))]
public class DebugController : MonoBehaviour
{
    [SerializeField] private DebugWindow _debugWindowPrefab = default;

    private DebugControls _controls = default;
    private DebugWindow _debugWindowInstance = default;

    private ReactiveProperty<bool> _showRays = new ReactiveProperty<bool>(false);
    public IObservable<bool> ShowRays => _showRays.ToReadOnlyReactiveProperty<bool>();

    private ReactiveProperty<bool> _unlimitedAmmos = new ReactiveProperty<bool>(false);
    public IObservable<bool> UnlimitedAmmos => _unlimitedAmmos.ToReadOnlyReactiveProperty<bool>();

    private ReactiveProperty<bool> _windowIsVisible = new ReactiveProperty<bool>(false);
    public IObservable<bool> WindowIsVisible => _windowIsVisible.ToReadOnlyReactiveProperty<bool>();

    private ReactiveProperty<int> _addMoneyToPlayer = new ReactiveProperty<int>(0);
    public IObservable<int> AddMoneyToPlayer => new ReadOnlyReactiveProperty<int>(_addMoneyToPlayer, false);


    private void Awake()
    {
        _controls = GetComponent<DebugControls>();
        _controls.WindowAction += DebugWindowActions;
    }

    private void DebugWindowActions()
    {
        if (_debugWindowInstance is null)
        {
            AddDebugWindowToScene();
            _windowIsVisible.Value = true;
        }
        else
        {
            _debugWindowInstance.ShowOrHide();
            _windowIsVisible.Value = !_windowIsVisible.Value;
        }

    }

    private void AddDebugWindowToScene()
    {
        _debugWindowInstance = Instantiate(_debugWindowPrefab);

        _debugWindowInstance.ShowRays.Subscribe(x => _showRays.Value = x);
        _debugWindowInstance.UnlimitedAmmos.Subscribe(x => _unlimitedAmmos.Value = x);
        _debugWindowInstance.AddMoneyToPlayer.Subscribe(x => _addMoneyToPlayer.SetValueAndForceNotify(x));
    } 
}
