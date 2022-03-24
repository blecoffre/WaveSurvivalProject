using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Zenject;

public class DebugControls : MonoBehaviour
{
    [Inject] private WaveSurvivalProject.DebugActions _inputs = default;
    //private ReactiveProperty<bool> _windowActions = new ReactiveProperty<bool>(false);
    //public IObservable<bool> WindowActions => _windowActions.ToReadOnlyReactiveProperty<bool>();
    public Action WindowAction;

    private void Awake()
    {
            
    }

    private void Start()
    {
        //_inputs.OpenDebug.performed += _ => _windowActions.Value = !_windowActions.Value;
        _inputs.OpenDebug.performed += _ => WindowAction.Invoke();
    }

    private void InitControls()
    {

    }
}
