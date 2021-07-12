using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Enemy : MonoBehaviour
{
    [Inject] private FloatVariable _enemyHP = default;

    private void Start()
    {
        Debug.Log(gameObject.name + "HP : " + _enemyHP.RuntimeValue);
    }
}
