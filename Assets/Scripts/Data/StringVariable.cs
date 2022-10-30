using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableValue/StringVariable", fileName = "StringVariable")]
public class StringVariable : ScriptableObject, ISerializationCallbackReceiver
{
    [SerializeField] private string InitialeValue;

    [NonSerialized]
    public ReactiveProperty<string> RuntimeValue;

    public void OnAfterDeserialize()
    {
        RuntimeValue = new ReactiveProperty<string>(InitialeValue);
    }

    public void OnBeforeSerialize() { }

    public void OverrideInitialValue(string newValue)
    {
        InitialeValue = newValue;
        RuntimeValue = new ReactiveProperty<string>(InitialeValue);
    }
}