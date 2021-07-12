using UnityEngine;
using Zenject;

public class GameMainInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<GameEvent>().WithId("PlayerHPChangedEvent").FromScriptableObjectResource("GameEvent/PlayerHPChanged").AsTransient();
        Container.Bind<FloatVariable>().FromScriptableObjectResource("PlayerData/PlayerHP").WhenInjectedInto(typeof(Player), typeof(DisplayPlayerHP), typeof(DecreasePlayerHP));
        Container.Bind<FloatVariable>().FromScriptableObjectResource("PlayerData/EnemyHP").WhenInjectedInto<Enemy>();
    }
}