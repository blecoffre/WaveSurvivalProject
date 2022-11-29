using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Shops/TurretShopDatas", fileName = "TurretShopDatas")]
public class TurretShopDatas : BaseShopDatas
{
    [SerializeField] private TurretShopSlotData[] _slotDatas = default;

    public TurretShopSlotData[] SlotDatas => _slotDatas;

    private TurretShopView _shopUI = default;

    private void Awake()
    {
        _shopUI = ShopView as TurretShopView;
    }
}
