using Michsky.MUIP;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class TurretShopSlotView : BaseShopSlotView
{
    public void SetItemData(TurretShopSlotData data)
    {
        base.SetItemData(data);  
    }

    public override void BuyItem()
    {
        _signalBus.AbstractFire(new TryBuyItemSignal { Data = _data as TurretShopSlotData });
    }
}
