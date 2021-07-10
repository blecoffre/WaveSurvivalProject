using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class DecreasePlayerHP : MonoBehaviour
{
    [Inject(Id = "PlayerHPVariable")] private FloatVariable _playerHp = default;
    [Inject(Id = "PlayerHPChangedEvent")] private GameEvent _playerHPChanged = default;

    private void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            _playerHp.RuntimeValue -= 1.0f;
            _playerHPChanged.Raise();
        }
    }
    
}
