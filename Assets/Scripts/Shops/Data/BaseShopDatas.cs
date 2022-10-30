using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseShopDatas : ScriptableObject
{
    [SerializeField][Tooltip("Name showed as title")] private string _shopName = default;
    [SerializeField] private BaseShopView _shopView = default;

    public string ShopName => _shopName;
    public BaseShopView ShopView => _shopView;
}
