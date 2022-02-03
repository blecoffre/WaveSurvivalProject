using UnityEngine;
using Zenject;

public class DebugInstaller : MonoInstaller
{
    [SerializeField] private RangeWeaponDrawDebugRay _rangeWeaponDebugRay = default;

    private WaveSurvivalProject _controls = default;

    public override void InstallBindings()
    {
        Container.Bind<WaveSurvivalProject.DebugActions>().FromInstance(_controls.Debug).AsSingle();

        Container.Bind<RangeWeaponDrawDebugRay>().FromInstance(_rangeWeaponDebugRay).AsSingle().WhenInjectedInto<RangeWeapon>();
    }

    [Inject]
    private void InitDebugControls(WaveSurvivalProject controls)
    {
        _controls = controls;
        _controls.Debug.Enable();
    }

    private void OnEnable()
    {
        if(_controls != null)
            _controls.Debug.Enable();
    }

    private void OnDisable()
    {
        if (_controls != null)
            _controls.Debug.Disable();
    }
}