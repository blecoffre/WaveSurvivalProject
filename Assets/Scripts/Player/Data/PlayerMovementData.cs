using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

[CreateAssetMenu(menuName = "WaveSurvival/PlayerData/PlayerMovementData")]
public class PlayerMovementData : ScriptableObject
{
    [SerializeField] private FloatVariable _playerWalkSpeed = default;
    [SerializeField] private FloatVariable _playerRunSpeed = default;
    [SerializeField] private FloatVariable _playerJumpVelocity = default;

    public ReactiveProperty<float> GetWalkSpeed()
    {
        return _playerWalkSpeed.RuntimeValue;
    }

    public ReactiveProperty<float> GetRunSpeed()
    {
        return _playerRunSpeed.RuntimeValue;
    }

    public ReactiveProperty<float> GetJumpVelocity()
    {
        return _playerJumpVelocity.RuntimeValue;
    }
}
