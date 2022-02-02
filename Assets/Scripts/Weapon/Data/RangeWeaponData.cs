using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Weapon/RangeData", fileName = "RangeWeaponData")]
public class RangeWeaponData : WeaponBaseData
{
    #region Ammo
    [SerializeField] private FloatVariable _currentAmmoInMagazine;
    [SerializeField] private FloatVariable _magazineCapacity;
    [SerializeField] private FloatVariable _remainingAmmo;
    #endregion

    [SerializeField] private FloatVariable _bulletSpread = default;
    [SerializeField] private SpreadType _spreadType = default;

    #region Ammo getter
    public ReactiveProperty<float> GetCurrentMagazine()
    {
        return _currentAmmoInMagazine.RuntimeValue;
    }

    public ReactiveProperty<float> GetMagazineCapacity()
    {
        return _magazineCapacity.RuntimeValue;
    }

    public ReactiveProperty<float> GetRemainingAmmo()
    {
        return _remainingAmmo.RuntimeValue;
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

        CreateDataAndBind("BulletSpread", path, folderName, out _bulletSpread);
    }
#endif
}
