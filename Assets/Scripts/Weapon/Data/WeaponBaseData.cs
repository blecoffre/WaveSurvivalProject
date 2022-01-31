using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UniRx;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Weapon/BaseData", fileName = "WeaponData")]
public class WeaponBaseData : ScriptableObject
{
    #region Shooting relative
    [SerializeField] private FloatVariable _range;
    [SerializeField] private FloatVariable _damage;
    [SerializeField] private FloatVariable _fireRate;
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

    protected virtual void Awake()
    {
        InitData();
    }

    protected virtual void InitData()
    {

    }

#if UNITY_EDITOR
    #region Auto create data
    [ContextMenu("Create all relative data")]
    protected virtual void CreateAllData()
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

        DataSet(path, folderName);
    }

    protected virtual void DataSet(string path, string folderName)
    {
        CreateDataAndBind("Range", path, folderName, out _range);
        CreateDataAndBind("Damage", path, folderName, out _damage);
        CreateDataAndBind("FireRate", path, folderName, out _fireRate);
    }

    protected void CreateDataAndBind(string dataName, string path, string folderName, out FloatVariable dataToSet)
    {
        FloatVariable asset = default;
        string assetName = string.Format("{0}_{1}", folderName, dataName);
        string assetPath = string.Format("{0}/{1}.asset", path, assetName);

        string[] ids = AssetDatabase.FindAssets(dataName,  new[] { path });
        if (ids.Length == 0)
        {
            asset = CreateInstance<FloatVariable>();
            
            AssetDatabase.CreateAsset(asset, assetPath);
            AssetDatabase.SaveAssets();
        }
        else
        {
            asset = AssetDatabase.LoadAssetAtPath<FloatVariable>(assetPath);
        }
        
        dataToSet = asset;
    }
    #endregion
#endif
}
