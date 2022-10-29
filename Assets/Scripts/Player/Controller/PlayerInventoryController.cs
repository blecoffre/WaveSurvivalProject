using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Zenject;
using System.Linq;
using Cysharp.Threading.Tasks;

public class PlayerInventoryController : MonoBehaviour
{
    private List<Weapon> _weapons =  new List<Weapon>();
    [SerializeField] private Transform _weaponBag = default;
    [SerializeField] private Transform _inventoryContainer = default;

    public ReactiveProperty<Weapon> CurrentSelectedWeapon = new ReactiveProperty<Weapon>();
    private ReactiveProperty<int> _playerMoney = new ReactiveProperty<int>(0);
    public IObservable<int> PlayerMoney => new ReadOnlyReactiveProperty<int>(_playerMoney, false);
    private ReactiveProperty<int> _playerGain = new ReactiveProperty<int>(0);
    public IObservable<int> PlayerGain => new ReadOnlyReactiveProperty<int>(_playerGain, false);

    public ReactiveProperty<Weapon> CollectedWeapon = new ReactiveProperty<Weapon>();

    [Inject] private readonly WeaponFactory _weaponFactory;

    [Inject] private PlayerPortraitInfos _moneyView = default;

#if UNITY_EDITOR
    #region Debug
    [Inject] protected DebugController _debug;
    private bool _debugOpened = false;
    #endregion
#endif

    [Inject]
    private void TakeStartingKit(StarterKit kit)
    {
        if(kit != null)
        {
            InstantiateWeapons(kit.GetStartingWeapons());
        }
    }

    private void Start()
    {
#if UNITY_EDITOR
        _debug.WindowIsVisible.Subscribe(x => _debugOpened = x);
        _debug.AddMoneyToPlayer.Subscribe(x => AddMoney(x));
#endif

        CollectedWeapon.Skip(1).Subscribe(x => CollectWeapon(x).Forget());
    }

    private void InstantiateWeapons(Weapon[] weapons)
    {
        foreach(Weapon w in weapons)
        {
            var weapon = _weaponFactory.Create(w);
            weapon.transform.SetParent(_weaponBag);
            weapon.transform.localPosition = Vector3.zero;
            weapon.transform.localRotation = Quaternion.identity;
            _weapons.Add(weapon);
        }
    }

    private void RemoveWeapon(int index)
    {
        _weapons.RemoveAt(index);
    }

    private void Awake()
    {
        SetCurrentWeapon(0);
    }

    private void SetCurrentWeapon(int index)
    {
        if(_weapons.Count > index)
        {
            CurrentSelectedWeapon.Value = _weapons[index];
        }
    }

    private void AddMoney(int gain)
    {
        _playerGain.SetValueAndForceNotify(gain);
        AddToWallet(gain);
        UpdateMoneyView(gain);
    }

    private void AddToWallet(int gain)
    {
        _playerMoney.SetValueAndForceNotify(_playerMoney.Value + gain);
    }

    private void UpdateMoneyView(int gain)
    {
        _moneyView.UpdateMoney(_playerMoney.Value);
    }

    private async UniTask CollectWeapon(Weapon _data)
    {
        InstantiateWeapons(new Weapon[] { _data });
        await UniTask.DelayFrame(5);
        SetCurrentWeapon(_weapons.IndexOf(_weapons.FirstOrDefault(x => x.Data.WeaponName == _data.Data.WeaponName)));
    }
}
