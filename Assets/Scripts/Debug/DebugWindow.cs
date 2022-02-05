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
    private ReactiveProperty<bool> _showRays = new ReactiveProperty<bool>(false);
    public IObservable<bool> ShowRays => _showRays.ToReadOnlyReactiveProperty<bool>();

    private void Start()
    {
        InitOptionsCallback();
    }

    private void InitOptionsCallback()
    {
        _weaponRay.onValueChanged.AddListener(delegate { ShowRaysChange(); });
    }

    private void ShowRaysChange()
    {
        _showRays.Value = !_showRays.Value;
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
