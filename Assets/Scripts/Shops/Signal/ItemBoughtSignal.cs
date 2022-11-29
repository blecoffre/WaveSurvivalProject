using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBoughtSignal : IItemBoughtSignal
{
    public BaseShopSlotData Data { get; set; }
}
