using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyData : MonoBehaviour
{
    [SerializeField] private FloatVariable _health = default;
    [SerializeField] private FloatVariable _attack = default;
    [SerializeField] private IntVariable _moneyLoot = default;
}
