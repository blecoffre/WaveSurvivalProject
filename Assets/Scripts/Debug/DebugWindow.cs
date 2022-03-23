using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class DebugWindow : MonoBehaviour
{
    [SerializeField] private GameObject _container = default;

    [SerializeField] private Toggle _weaponRay = default;
    [SerializeField] private Toggle _unlimitedAmmosCheat = default;

    private ReactiveProperty<bool> _showRays = new ReactiveProperty<bool>(false);
    public IObservable<bool> ShowRays => _showRays.ToReadOnlyReactiveProperty<bool>();

    private ReactiveProperty<bool> _unlimitedAmmos = new ReactiveProperty<bool>(false);
    public IObservable<bool> UnlimitedAmmos => _unlimitedAmmos.ToReadOnlyReactiveProperty<bool>();

    private void Start()
    {
        InitOptionsCallback();
    }

    private void InitOptionsCallback()
    {
        _weaponRay.onValueChanged.AddListener(delegate { ShowRaysChange(); });
        _unlimitedAmmosCheat.onValueChanged.AddListener(delegate { UnlimitedAmmoChange(); });
    }

    private void ShowRaysChange()
    {
        _showRays.Value = !_showRays.Value;
    }

    private void UnlimitedAmmoChange()
    {
        _unlimitedAmmos.Value = !_unlimitedAmmos.Value;
    }

    public void ShowOrHide()
    {
        if (_container.activeInHierarchy)
        {
            Hide();
        }
        else
        {
            Show();
        }
    }

    private void Hide()
    {
        _container.SetActive(false);
    }

    private void Show()
    {
        _container.SetActive(true);
    }

    private void Close()
    {
        //Disable all affected
        Destroy(gameObject);
    }
}
