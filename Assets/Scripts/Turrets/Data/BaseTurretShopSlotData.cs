using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/ShopSlot/BaseTurretShopSlotData", fileName = "BaseTurretShopSlotData")]
public class BaseTurretShopSlotData : ScriptableObject
{
    [SerializeField] private StringVariable _name = default;
    [SerializeField] private BaseTurretData _baseData = default;
    [SerializeField] private IntVariable _price = default;
    [SerializeField] private Sprite _slotIcon = default;

    public StringVariable Name => _name;
    public BaseTurretData BaseData => _baseData;
    public IntVariable Price => _price;
    public Sprite SlotIcon => _slotIcon;
}
