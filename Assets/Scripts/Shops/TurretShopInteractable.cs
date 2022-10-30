using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

public class TurretShopInteractable : Interactable
{
    [SerializeField] private TurretShopDatas _shop;

    [Inject] private ShopsManager _shopsManager = default;

    public async override Task<bool> Interact()
    {
        await base.Interact();
        bool isValid = await _shopsManager.ShopRequest(_shop);
        return isValid;
    }
}