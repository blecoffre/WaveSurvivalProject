using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Shops/WeaponShopDatas", fileName = "WeaponShopDatas")]
public class WeaponShopDatas : BaseShopDatas
{
    private WeaponShopView _shopUI = default;
    private void Awake()
    {
        _shopUI = ShopView as WeaponShopView;
    }
}
