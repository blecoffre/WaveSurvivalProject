using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

[CreateAssetMenu(menuName = "WaveSurvival/PlayerData/PlayerLookData")]
public class PlayerLookData : ScriptableObject
{
    [SerializeField] private FloatVariable _sensitivityX = default;
    [SerializeField] private FloatVariable _sensitivityY = default;

    public ReactiveProperty<float> GetXSensitivity()
    {
        return _sensitivityX.RuntimeValue;
    }

    public ReactiveProperty<float> GetYSensitivity()
    {
        return _sensitivityY.RuntimeValue;
    }
}
