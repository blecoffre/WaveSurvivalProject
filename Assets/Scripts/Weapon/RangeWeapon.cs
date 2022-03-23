using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Zenject;


public class RangeWeapon : Weapon, IWeapon, IReloadable
{
    [SerializeField] private Transform _end;
    [Inject] private RangeWeaponDrawDebugRay _debugRay;

    private RangeWeaponData _rangeData;

    public int CurrentAmmo => _rangeData.GetCurrentMagazine().Value;
    public int RemainingAmmo => _rangeData.GetRemainingAmmo().Value;

    #region Debug
    private bool _showDebugRays = false;
    #endregion

    protected override void Start()
    {
        base.Start();
        _rangeData = _data as RangeWeaponData;

        float timeBetweenShot = 1 / _data.GetFireRate().Value;
        _attackDisposable = Observable.EveryUpdate().Where(_ => _attackPressed.Value == true && HasEnoughAmmo()).ThrottleFirst(TimeSpan.FromMilliseconds(timeBetweenShot)).Subscribe(_ =>
        {
            Shoot();
            ConsumeAmmo();
        });

#if UNITY_EDITOR
        DebugBinding();
#endif
    }

#if UNITY_EDITOR
    private void DebugBinding()
    {
        _debug.ShowRays.Subscribe(x => _showDebugRays = x);
    }
#endif

    public override void Attack()
    {
        base.Attack();
    }

    protected void Shoot()
    {
        Debug.Log("Attack");
        Vector3 rayOrigin = _camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
        Vector3 forwardDir = Vector3.one;

        switch (_rangeData.GetCurrentSpreadType())
        {
            case SpreadType.Angle_Deviation:
                forwardDir = AngleDeviationMethod();
                break;
            case SpreadType.Deviation_Distance:
                forwardDir = DeviationDistanceMethod();
                break;
        }

        RaycastHit hit;
        if (Physics.Raycast(_camera.transform.position, forwardDir, out hit, _data.GetRange().Value))
        {
            IDamageable<float> damageable = hit.transform.GetComponent<IDamageable<float>>();
            if (damageable != null)
            {
                damageable.TakeDamage(_data.GetDamage().Value);
            }

#if UNITY_EDITOR
            DrawDebugRay(_end.position, hit.point);
#endif
        }
        else
        {
#if UNITY_EDITOR
            DrawDebugRay(_end.position, rayOrigin + (forwardDir * _data.GetRange().Value));
#endif
        }
    }

    public override void StopAttack()
    {
        base.StopAttack();
    }

    private void DrawDebugRay(Vector3 startPoint, Vector3 endPoint)
    {
        if (_showDebugRays)
        {
            RangeWeaponDrawDebugRay line = Instantiate(_debugRay);
            line.DrawRay(new Vector3[] { startPoint, endPoint }, 0.1f);
            //Debug.DrawRay(_camera.transform.position, _camera.transform.forward * _range, Color.blue, 2.5f);
        }
    }

    private Vector3 AngleDeviationMethod()
    {
        Vector3 forwardVector = Vector3.forward;
        float deviation = UnityEngine.Random.Range(0f, _rangeData.GetBulletSpread().Value);
        float angle = UnityEngine.Random.Range(0f, 360f);
        forwardVector = Quaternion.AngleAxis(deviation, Vector3.up) * forwardVector;
        forwardVector = Quaternion.AngleAxis(angle, Vector3.forward) * forwardVector;
        forwardVector = _camera.transform.rotation * forwardVector;

        return forwardVector;
    }

    private Vector3 DeviationDistanceMethod()
    {
        Vector3 deviation3D = UnityEngine.Random.insideUnitCircle * _rangeData.GetBulletSpread().Value;
        Quaternion rot = Quaternion.LookRotation(Vector3.forward * _data.GetRange().Value + deviation3D);
        Vector3 forwardVector = _camera.transform.rotation * rot * Vector3.forward;

        return forwardVector;
    }

    private bool HasEnoughAmmo()
    {
        return _rangeData.GetCurrentMagazine().Value >= _rangeData.GetAmmoConsumptionPerAttack().Value;
    }

    private void ConsumeAmmo()
    {
        int newCurrentAmmoCount = _rangeData.GetCurrentMagazine().Value - _rangeData.GetAmmoConsumptionPerAttack().Value;
        _rangeData.SetCurrentMagazine(newCurrentAmmoCount);
    }

    public IObservable<int> AmmoConsumed()
    {
        return _rangeData.GetCurrentMagazine().ToReadOnlyReactiveProperty();
    }

    private bool HasAmmoToReload()
    {
        return _rangeData.GetRemainingAmmo().Value > 0;
    }

    public void Reload()
    {
        if (HasAmmoToReload())
        {
            int missingAmmo = _rangeData.GetMagazineCapacity().Value - _rangeData.GetCurrentMagazine().Value;

            if(_rangeData.GetRemainingAmmo().Value >= missingAmmo)
            {
                _rangeData.SetRemainingAmmo(_rangeData.GetRemainingAmmo().Value - missingAmmo);
                _rangeData.SetCurrentMagazine(_rangeData.GetCurrentMagazine().Value + missingAmmo);
            }
            else
            {
                _rangeData.SetRemainingAmmo(0);
                _rangeData.SetCurrentMagazine(_rangeData.GetCurrentMagazine().Value + _rangeData.GetRemainingAmmo().Value);
            }
        }
    }
}
