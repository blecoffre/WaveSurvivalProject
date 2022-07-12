using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UniRx;
using UnityEngine.Animations.Rigging;

public class PlayerWeapon : MonoBehaviour
{
    private Weapon _currentWeapon = default;
    [Inject] private PlayerWeaponHUD _hud = default;

    [SerializeField] private Transform _weaponParent = default;
    [SerializeField] private Rig _handIK = default;

    [SerializeField] private Animator _rigController = default;

    private void Awake()
    {

        if (_currentWeapon is null)
        {
            _handIK.weight = 0.0f;
        }

    }

    public void SetCurrentWeapon(Weapon weapon)
    {
        if(weapon != null)
        {
            _currentWeapon = weapon;

            if (_currentWeapon is RangeWeapon)
            {
                var rangeW = _currentWeapon as RangeWeapon;
                rangeW.AmmoConsumed().Subscribe(_ => _hud.UpdateAmmo(rangeW.CurrentAmmo, rangeW.RemainingAmmo));
            }
            _handIK.weight = 1.0f;
            _rigController.Play("Equip_" + _currentWeapon.Data.WeaponName);
        }
    }

    public void Attack()
    {
        _currentWeapon.Attack();
    }

    public void StopAttack()
    {
        _currentWeapon.StopAttack();
    }

    public void Reload()
    {
        if (_currentWeapon.GetComponent<IReloadable>() != null)
        {
            _currentWeapon.GetComponent<IReloadable>().Reload();
        }
    }
}
