using UnityEngine;
using Zenject;

public class GameMainInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<GameEvent>().WithId("PlayerHPChangedEvent").FromScriptableObjectResource("GameEvent/PlayerHPChanged").AsTransient();
        Container.Bind<FloatVariable>().FromScriptableObjectResource("PlayerData/EnemyHP").WhenInjectedInto<Enemy>();
    }
}