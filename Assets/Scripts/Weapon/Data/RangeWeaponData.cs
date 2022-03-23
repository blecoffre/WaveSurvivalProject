using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Weapon/RangeData", fileName = "RangeWeaponData")]
public class RangeWeaponData : WeaponBaseData
{
    #region Ammo
    [SerializeField] private IntVariable _currentAmmoInMagazine;
    [SerializeField] private IntVariable _magazineCapacity;
    [SerializeField] private IntVariable _remainingAmmo;
    [SerializeField] private IntVariable _ammoConsumptionPerAttack;
    #endregion

    [SerializeField] private FloatVariable _bulletSpread = default;
    [SerializeField] private SpreadType _spreadType = default;

    #region Ammo getter
    public ReactiveProperty<int> GetCurrentMagazine()
    {
        return _currentAmmoInMagazine.RuntimeValue;
    }

    public ReactiveProperty<int> GetMagazineCapacity()
    {
        return _magazineCapacity.RuntimeValue;
    }

    public ReactiveProperty<int> GetRemainingAmmo()
    {
        return _remainingAmmo.RuntimeValue;
    }

    public ReactiveProperty<int> GetAmmoConsumptionPerAttack()
    {
        return _ammoConsumptionPerAttack.RuntimeValue;
    }
    #endregion

    #region Ammo setter
    public void SetCurrentMagazine(int newValue)
    {
        _currentAmmoInMagazine.RuntimeValue.Value = newValue;
    }

    public void SetMagazineCapacity(int newValue)
    {
        _magazineCapacity.RuntimeValue.Value = newValue;
    }

    public void SetRemainingAmmo(int newValue)
    {
        _remainingAmmo.RuntimeValue.Value = newValue;
    }

    public void SetAmmoConsumptionPerAttack(int newValue)
    {
        _ammoConsumptionPerAttack.RuntimeValue.Value = newValue;
    }
    #endregion

    public ReactiveProperty<float> GetBulletSpread()
    {
        return _bulletSpread.RuntimeValue;
    }

    public SpreadType GetCurrentSpreadType()
    {
        return _spreadType;
    }

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void InitData()
    {
        base.InitData();

        InitAmmoData();
    }

    private void InitAmmoData()
    {
        _currentAmmoInMagazine.OverrideInitialValue(_magazineCapacity.RuntimeValue.Value);
    }

#if UNITY_EDITOR
    protected override void DataSet(string path, string folderName)
    {
        base.DataSet(path, folderName);

        CreateDataAndBind("CurrentAmmoInMag", path, folderName, out _currentAmmoInMagazine);
        CreateDataAndBind("MagCapacity", path, folderName, out _magazineCapacity);
        CreateDataAndBind("RemainingAmmo", path, folderName, out _remainingAmmo);
        CreateDataAndBind("AmmoConsumption", path, folderName, out _ammoConsumptionPerAttack);

        CreateDataAndBind("BulletSpread", path, folderName, out _bulletSpread);
    }
#endif
}
