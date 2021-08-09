using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class EnemyAttackComponent : MonoBehaviour, IAttack
{
    [SerializeField] private string _baseAttackAnimationParameter = default;
    private float _attackRange = 10.0f;
    private Transform _mobTr = default;
    private Transform _target = default;

    public void SetDatas()
    {
        Init();
    }

    private void Init()
    {
        _mobTr = transform;

        Observable.EveryUpdate().Where(_ => Vector3.Distance(_mobTr.position, _target.position) <= _attackRange).Subscribe(_ => Attack());
    }

    private void Update()
    {
        _target = FindObjectOfType<Player>().transform;
    }

    public void Attack()
    {
        GetComponent<Animator>().SetBool("Bite Attack", true);
    }
}
