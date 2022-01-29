using UnityEngine;
using Zenject;

public class DebugInstaller : MonoInstaller
{
    [SerializeField] private RangeWeaponDrawDebugRay _rangeWeaponDebugRay = default;

    public override void InstallBindings()
    {
        Container.Bind<RangeWeaponDrawDebugRay>().FromInstance(_rangeWeaponDebugRay).AsSingle().WhenInjectedInto<RangeWeapon>();
    }
}