using UnityEngine;
using UnityEngine.Animations.Rigging;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<PlayerController>().FromComponentOn(gameObject).AsSingle();

        Container.Bind<PlayerMovement>().FromComponentOn(gameObject).AsSingle();
        Container.Bind<PlayerMovementData>().FromScriptableObjectResource("PlayerData/PlayerMovement/PlayerMovementData").WhenInjectedInto<PlayerMovement>();
        Container.Bind<CharacterController>().FromComponentOn(gameObject).AsSingle();

        Container.Bind<Camera>().FromComponentInChildren().AsSingle();
        Container.Bind<PlayerLook>().FromComponentOn(gameObject).AsSingle();
        Container.Bind<PlayerLookData>().FromScriptableObjectResource("PlayerData/PlayerLook/PlayerLookData").WhenInjectedInto<PlayerLook>();

        Container.Bind<FloatVariable>().FromScriptableObjectResource("PlayerData/PlayerHP").WhenInjectedInto(typeof(Player));
        Container.Bind<PlayerWeapon>().FromComponentInChildren().AsSingle();
        Container.Bind<PlayerWeaponHUD>().FromComponentInHierarchy().AsSingle();
        Container.Bind<PlayerPortraitInfos>().FromComponentInHierarchy().AsSingle();

        Container.BindFactory<UnityEngine.Object, Weapon, WeaponFactory>().FromFactory<PrefabFactory<Weapon>>();

        Container.Bind<RigBuilder>().FromComponentInHierarchy().AsSingle();
    }
}