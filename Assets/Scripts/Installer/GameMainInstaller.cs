using UnityEngine;
using Zenject;

public class GameMainInstaller : MonoInstaller
{
    private WaveSurvivalProject _controls = default;

    public override void InstallBindings()
    {
        InitializeInputs();
        Container.Bind<WaveSurvivalProject>().FromInstance(_controls).AsSingle();
        Container.Bind<WaveSurvivalProject.PlayerActions>().FromInstance(_controls.Player).AsSingle().WhenInjectedInto<PlayerController>();

        //Container.Bind<GameEvent>().WithId("PlayerHPChangedEvent").FromScriptableObjectResource("GameEvent/PlayerHPChanged").AsTransient();
        Container.Bind<FloatVariable>().FromScriptableObjectResource("EnemyData/EnemyHP").WhenInjectedInto<Enemy>();
    }

    private void InitializeInputs()
    {
        _controls = new WaveSurvivalProject();
    }
}