using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UniRx;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/WeaponBaseData", fileName = "WeaponData")]
public class WeaponBaseData : ScriptableObject
{
    #region Shooting relative
    [SerializeField] private FloatVariable _range;
    [SerializeField] private FloatVariable _damage;
    [SerializeField] private FloatVariable _fireRate;
    #endregion

    #region Ammo
    [SerializeField] private FloatVariable _currentAmmoInMagazine;
    [SerializeField] private FloatVariable _magazineCapacity;
    [SerializeField] private FloatVariable _remainingAmmo;
    #endregion

    #region Attachements
    #endregion

    #region Uppgrade
    #endregion

    #region Skin
    [SerializeField] private Material[] _skins;
    #endregion

    #region Shooting relative getter
    public ReactiveProperty<float> GetRange()
    {
        return _range.RuntimeValue;
    }

    public ReactiveProperty<float> GetDamage()
    {
        return _damage.RuntimeValue;
    }

    public ReactiveProperty<float> GetFireRate()
    {
        return _fireRate.RuntimeValue;
    }

    #endregion

    #region Ammo getter
    public ReactiveProperty<float> GetCurrentMagazine()
    {
        return _damage.RuntimeValue;
    }

    public ReactiveProperty<float> GetMagazineCapacity()
    {
        return _damage.RuntimeValue;
    }

    public ReactiveProperty<float> GetRemainingAmmo()
    {
        return _damage.RuntimeValue;
    }
    #endregion

    private void Awake()
    {
        InitData();
    }

    private void InitData()
    {
        InitAmmoData();
    }

    private void InitAmmoData()
    {
        _currentAmmoInMagazine.OverrideInitialValue(_magazineCapacity.RuntimeValue.Value);
    }

#if UNITY_EDITOR
    #region Auto create data
    [ContextMenu("Create all relative data")]
    private void CreateAllData()
    {
        string path = AssetDatabase.GetAssetPath(this);
        if (path == "")
        {
            path = "Assets";
        }
        else if (Path.GetExtension(path) != "")
        {
            path = path.Replace(Path.GetFileName(AssetDatabase.GetAssetPath(Selection.activeObject)), "");
        }
        var split = path.Split('/');
        string folderName = split[split.Length - 2];

        CreateDataAndBind("Range", path, folderName, out _range);
        CreateDataAndBind("Damage", path, folderName, out _damage);
        CreateDataAndBind("FireRate", path, folderName, out _fireRate);
        CreateDataAndBind("CurrentAmmoInMag", path, folderName, out _currentAmmoInMagazine);
        CreateDataAndBind("MagCapacity", path, folderName, out _magazineCapacity);
        CreateDataAndBind("RemainingAmmo", path, folderName, out _remainingAmmo);
    }

    private void CreateDataAndBind(string dataName, string path, string folderName, out FloatVariable dataToSet)
    {
        FloatVariable asset = CreateInstance<FloatVariable>();
        string assetName = string.Format("{0}_{1}", folderName, dataName);
        string assetPath = string.Format("{0}/{1}.asset", path, assetName);
        AssetDatabase.CreateAsset(asset, assetPath);
        AssetDatabase.SaveAssets();

        dataToSet = asset;
    }
    #endregion
#endif
}
