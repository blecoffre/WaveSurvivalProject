using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Zenject;


public class RangeWeapon : Weapon
{
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

        RaycastHit hit;
        if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, out hit, _data.GetRange().Value))
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
            DrawDebugRay(_end.position, rayOrigin + (_camera.transform.forward * _data.GetRange().Value));
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
}
