using UnityEngine;
using Zenject;

public class PlayerInventoryInstaller : MonoInstaller
{
    [SerializeField] private StarterKit _playerStarterKit = default;

    public override void InstallBindings()
    {
        Container.Bind<StarterKit>().FromInstance(_playerStarterKit).AsSingle();
        Container.Bind<PlayerInventoryController>().FromComponentInHierarchy().AsSingle();
        Container.Bind<PlayerInventoryFeedback>().FromComponentInHierarchy().AsSingle();
    }
}