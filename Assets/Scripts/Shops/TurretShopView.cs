using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretShopView : BaseShopView
{
    [SerializeField] private TurretShopSlotView _slotViewPrefab = default;
    [SerializeField] private Transform _container = default;

    public override void Initialize(BaseShopDatas data)
    {
        base.Initialize(data);
        TurretShopDatas tData = data as TurretShopDatas;
        CreateSlots(tData.SlotDatas);
    }
    
    private void CreateSlots(TurretShopSlotData[] slotsData)
    {
        foreach(TurretShopSlotData slotData in slotsData)
        {
            TurretShopSlotView view = Instantiate(_slotViewPrefab, _container);
            view.SetItemData(slotData as TurretShopSlotData);
        }
    }
}
