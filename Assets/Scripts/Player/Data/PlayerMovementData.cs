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
    [SerializeField] private FloatVariable _animationBlendSpeed = default;
    [SerializeField] private FloatVariable _groundDrag = default;
    [SerializeField] private FloatVariable _airMultiplier = default;
    [SerializeField] private FloatVariable _rotationSpeed = default;

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

    public ReactiveProperty<float> GetAnimationBlendSpeed()
    {
        return _animationBlendSpeed.RuntimeValue;
    }

    public ReactiveProperty<float> GetGroundDrag()
    {
        return _groundDrag.RuntimeValue;
    }

    public ReactiveProperty<float> GetAirMultiplier()
    {
        return _airMultiplier.RuntimeValue;
    }

    public ReactiveProperty<float> GetRotationSpeed()
    {
        return _rotationSpeed.RuntimeValue;
    }
}
