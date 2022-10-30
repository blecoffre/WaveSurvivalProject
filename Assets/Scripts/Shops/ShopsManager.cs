using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Zenject;

public class ShopsManager : IInitializable
{
    public IObservable<BaseShopDatas> OnOpenShop => _onOpenShop;
    public IObservable<BaseShopView> OnCloseShop => _onCloseShop;

    private BaseShopDatas _currentShopInstance = default;
    private Subject<BaseShopDatas> _onOpenShop = new Subject<BaseShopDatas>();
    private Subject<BaseShopView> _onCloseShop = new Subject<BaseShopView>();

    public void Initialize()
    {
        _currentShopInstance = null;
    }

    public UniTask<bool> ShopRequest(BaseShopDatas shopToOpen)
    {
        if (_currentShopInstance is null)
        {
            _currentShopInstance = shopToOpen;
            _onOpenShop.OnNext(shopToOpen);

            return new UniTask<bool>(true);
        }

        return new UniTask<bool>(true);
    }

    public void CloseShop(BaseShopView view)
    {
        if(_currentShopInstance.ShopView.GetType() == view.GetType())
        {
            _currentShopInstance = null;
            view.Close();
            _onCloseShop.OnNext(view);
        }
    }
}
