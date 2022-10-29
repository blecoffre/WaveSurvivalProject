using UnityEngine;
using UnityEngine.Animations.Rigging;
using Zenject;
using WeWillSurvive;

public class PlayerInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
#region Bind Data first

        Container.Bind<PlayerMovementData>().FromScriptableObjectResource("PlayerData/PlayerMovement/_PlayerMovementData").WhenInjectedInto<PlayerMovementController>();
        Container.Bind<PlayerLookData>().FromScriptableObjectResource("PlayerData/PlayerLook/_PlayerLookData"). AsSingle();
        Container.Bind<FloatVariable>().FromScriptableObjectResource("PlayerData/PlayerHP").WhenInjectedInto(typeof(Player));

#endregion

        Container.Bind<PlayerController>().FromComponentInHierarchy().AsSingle();

#region Movement

        Container.Bind<PlayerMovementController>().FromComponentInHierarchy().AsSingle();
        Container.Bind<Rigidbody>().FromComponentInHierarchy().AsSingle();

#endregion


#region Animation

        Container.Bind<PlayerAnimatorController>().FromComponentInHierarchy().AsSingle();
        Container.Bind<Animator>().FromComponentInHierarchy().AsSingle();

#endregion



        //Container.Bind<Camera>().FromComponentInChildren().AsSingle();
        Container.Bind<PlayerLookController>().FromComponentInHierarchy().AsSingle();
        Container.Bind<PlayerInteractionController>().FromComponentInHierarchy().AsSingle();

        Container.Bind<PlayerWeaponController>().FromComponentInHierarchy().AsSingle();
        Container.Bind<PlayerWeaponHUD>().FromComponentInHierarchy().AsSingle();
        Container.Bind<PlayerPortraitInfos>().FromComponentInHierarchy().AsSingle();


        Container.BindFactory<UnityEngine.Object, Weapon, WeaponFactory>().FromFactory<PrefabFactory<Weapon>>();

        Container.Bind<RigBuilder>().FromComponentInHierarchy().AsSingle();
    }
}