using UnityEngine;
using Zenject;

public class ShopInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<ShopsManager>().AsSingle();
        Container.Bind<ShopsController>().FromComponentInChildren().AsSingle();
    }
}