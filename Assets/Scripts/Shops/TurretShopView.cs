using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretShopView : BaseShopView
{
    [SerializeField] private TurretShopSlotView _slotViewPrefab = default;
    [SerializeField] private Transform _container = default;

    public override void Initialize(BaseShopDatas data)
    {
        TurretShopDatas tData = data as TurretShopDatas;
        CreateSlots(tData.SlotDatas);
    }
    
    private void CreateSlots(BaseTurretShopSlotData[] slotsData)
    {
        foreach(BaseTurretShopSlotData slotData in slotsData)
        {
            TurretShopSlotView view = Instantiate(_slotViewPrefab, _container);
            view.Initialize(slotData);
        }
    }
}
