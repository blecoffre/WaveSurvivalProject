using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseShopSlotData : ScriptableObject, IShopItem
{
    [SerializeField] private StringVariable _name = default;
    [SerializeField] private BaseTurretData _baseData = default;
    [SerializeField] private IntVariable _price = default;
    [SerializeField] private Sprite _slotIcon = default;

    public StringVariable Name => _name;
    public BaseTurretData BaseData => _baseData;
    public IntVariable Price => _price;
    public Sprite SlotIcon => _slotIcon;

    public void Buy()
    {
        throw new System.NotImplementedException();
    }
}
