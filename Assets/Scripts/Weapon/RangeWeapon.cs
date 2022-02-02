using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Zenject;


public class RangeWeapon : Weapon
{
    private RangeWeaponData _rangeData;

    private void Start()
    {
        _data = _data as RangeWeaponData;
    }

    [SerializeField] private Transform _end;
    [Inject] private RangeWeaponDrawDebugRay _debugRay;

    public override void Attack()
    {
        base.Attack();
        float timeBetweenShot = 1 / _data.GetFireRate().Value;
        Observable.EveryUpdate().Where(_ => _attackPressed.Value == true).ThrottleFirst(TimeSpan.FromMilliseconds(timeBetweenShot)).Subscribe(_ => Shoot());
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
            IDamageable damageable = hit.transform.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(_data.GetDamage().Value);
            }

            DrawDebugRay(_end.position, hit.point);
        }
        else
        {
            DrawDebugRay(_end.position, rayOrigin + (forwardDir * _data.GetRange().Value));
        }
    }

    public override void StopAttack()
    {
        base.StopAttack();
    }

    private void DrawDebugRay(Vector3 startPoint, Vector3 endPoint)
    {
        RangeWeaponDrawDebugRay line = Instantiate(_debugRay);
        line.DrawRay(new Vector3[] { startPoint, endPoint }, 0.1f);
        //Debug.DrawRay(_camera.transform.position, _camera.transform.forward * _range, Color.blue, 2.5f);
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
}
