using UnityEngine;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<PlayerMovement>().FromComponentOn(gameObject).AsSingle();
        Container.Bind<PlayerMovementData>().FromScriptableObjectResource("PlayerData/PlayerMovement/PlayerMovementData").WhenInjectedInto<PlayerMovement>();
        Container.Bind<CharacterController>().FromComponentOn(gameObject).AsSingle();



        Container.Bind<FloatVariable>().FromScriptableObjectResource("PlayerData/PlayerHP").WhenInjectedInto(typeof(Player));
        Container.Bind<PlayerController>().FromComponentOn(gameObject).AsSingle();
        Container.Bind<PlayerLook>().FromComponentOn(gameObject).AsSingle();
        Container.Bind<Weapon>().FromComponentInChildren().AsSingle();
        Container.Bind<Camera>().FromComponentInChildren().AsSingle();
    }
}