using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableValue/FloatVariable", fileName = "FloatVariable")]
public class FloatVariable : ScriptableObject, ISerializationCallbackReceiver
{
    [SerializeField] private float InitialeValue;

    [NonSerialized]
    public ReactiveProperty<float> RuntimeValue;

    public void OnAfterDeserialize()
    {
        RuntimeValue = new ReactiveProperty<float>(InitialeValue);
    }

    public void OnBeforeSerialize() { }

    public void OverrideInitialValue(float newValue)
    {
        InitialeValue = newValue;
        RuntimeValue = new ReactiveProperty<float>(InitialeValue);
    }
}
