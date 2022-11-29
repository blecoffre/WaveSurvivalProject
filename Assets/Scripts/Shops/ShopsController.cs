using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Cysharp.Threading.Tasks;
using UniRx;

public class ShopsController : MonoBehaviour
{
    [SerializeField] private Transform _container = default;

    private ShopsManager _manager = default;
    //private PlayerWallet _playerWallet = default;

    [Inject]
    private void Init(ShopsManager shopsManager/*, PlayerWallet playerWallet*/)
    {
        _manager = shopsManager;
        //_playerWallet = playerWallet;
    }

    private void Start()
    {
        _manager.OnOpenShop.Subscribe(shop =>
        {
            OpenShop(shop);
        }).AddTo(gameObject);

        _manager.OnCloseShop.Subscribe(_ =>
        {

        });


    }

    private void OpenShop(BaseShopDatas shop)
    {
        BaseShopView shopView = Instantiate(shop.ShopView, _container, false);
        shopView.Initialize(shop);
    }
}
