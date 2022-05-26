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

    [SerializeField] private Animator _animator = default;

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
            SetWeaponPosition(transform);
        }    
    }

    private void SetWeaponPosition(Transform container)
    {
        _currentWeapon.transform.position = container.position;
        _currentWeapon.transform.rotation = container.rotation;
        _currentWeapon.transform.localScale = container.localScale;
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
