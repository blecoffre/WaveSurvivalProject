using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UniRx;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IDamageable
{
    [Inject] private FloatVariable _enemyHP = default;
    private EnemyAttackComponent _attackComponent = default;
    private bool _isGrounded = default;

    [SerializeField] private Transform _feet = default;
    [SerializeField] private LayerMask _groundMask = default;

    private NavMeshAgent _agent = default;

    public void TakeDamage(float damage)
    {
        _enemyHP.RuntimeValue.Value -= damage;

        if (_enemyHP.RuntimeValue.Value <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        //_enemyHP = Instantiate(_enemyHP); //Create new instance of this scriptable object, made it unique for each enemy
        //_enemyHP.RuntimeValue.Where(x => x <= 0).Subscribe(_ => Die()).AddTo(this);
        _attackComponent = GetComponent<EnemyAttackComponent>();
        _attackComponent.SetDatas();

        _agent = GetComponent<NavMeshAgent>();
        _agent.enabled = false;
    }

    private void Update()
    {
        _isGrounded = Physics.CheckSphere(_feet.position, 0.1f, _groundMask);
        _agent.enabled = _isGrounded;

    }

    private void Die()
    {
        Destroy(gameObject);
    }

    private void Attack()
    {

    }
}
