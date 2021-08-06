using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UniRx;

public class Enemy : MonoBehaviour, IDamageable
{
    [Inject] private FloatVariable _enemyHP = default;

    public void TakeDamage(float damage)
    {
        _enemyHP.RuntimeValue.Value -= damage;
    }

    private void Start()
    {
        _enemyHP = Instantiate(_enemyHP); //Create new instance of this scriptable object, made it unique for each enemy
        _enemyHP.RuntimeValue.Where(x => x <= 0).Subscribe(_ => Die()).AddTo(this);
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
