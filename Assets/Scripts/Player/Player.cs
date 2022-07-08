using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace WeWillSurvive
{
    public class Player : MonoBehaviour
    {
        //[Inject] private FloatVariable _playerHP = default;

        private void Start()
        {
            //Debug.Log(gameObject.name + "HP : " + _playerHP.RuntimeValue);
        }
    }
}
