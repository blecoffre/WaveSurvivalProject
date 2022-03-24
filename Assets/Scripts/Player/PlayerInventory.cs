using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Zenject;

public class PlayerInventory : MonoBehaviour
{
    private List<Weapon> _weapons =  new List<Weapon>();
    [SerializeField] private Transform _playerHand = default;
    [SerializeField] private Transform _inventoryContainer = default;

    public ReactiveProperty<Weapon> CurrentSelectedWeapon = new ReactiveProperty<Weapon>();

    [Inject] private readonly WeaponFactory _weaponFactory;

    [Inject]
    private void TakeStartingKit(StarterKit kit)
    {
        InstantiateWeapons(kit.GetStartingWeapons());
    }

    private void InstantiateWeapons(Weapon[] weapons)
    {
        foreach(Weapon w in weapons)
        {
            var weapon = _weaponFactory.Create(w);
            weapon.transform.SetParent(_playerHand);
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
        CurrentSelectedWeapon.Value = _weapons[index];
    }
}
