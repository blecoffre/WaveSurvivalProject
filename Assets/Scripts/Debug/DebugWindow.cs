using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DebugWindow : MonoBehaviour
{
    [SerializeField] private GameObject _container = default;

    [SerializeField] private Toggle _weaponRay = default;
    [SerializeField] private Toggle _unlimitedAmmosCheat = default;
    [SerializeField] private Button _addMoneyToPlayerCheat = default;
    [SerializeField] private TMP_InputField _moneyAmount = default;

    private ReactiveProperty<bool> _showRays = new ReactiveProperty<bool>(false);
    public IObservable<bool> ShowRays => _showRays.ToReadOnlyReactiveProperty<bool>();

    private ReactiveProperty<bool> _unlimitedAmmos = new ReactiveProperty<bool>(false);
    public IObservable<bool> UnlimitedAmmos => _unlimitedAmmos.ToReadOnlyReactiveProperty<bool>();

    private ReactiveProperty<int> _addMoneyToPlayer = new ReactiveProperty<int>(0);
    public IObservable<int> AddMoneyToPlayer => new ReadOnlyReactiveProperty<int>(_addMoneyToPlayer, false);

    private void Start()
    {
        InitOptionsCallback();
    }

    private void InitOptionsCallback()
    {
        _weaponRay.onValueChanged.AddListener(delegate { ShowRaysChange(); });
        _unlimitedAmmosCheat.onValueChanged.AddListener(delegate { UnlimitedAmmoChange(); });
        _addMoneyToPlayerCheat.onClick.AddListener(() => AddMoneyChange());
    }

    private void ShowRaysChange()
    {
        _showRays.Value = !_showRays.Value;
    }

    private void UnlimitedAmmoChange()
    {
        _unlimitedAmmos.Value = !_unlimitedAmmos.Value;
    }

    private void AddMoneyChange()
    {
        _addMoneyToPlayer.SetValueAndForceNotify(int.Parse(_moneyAmount.text));
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
