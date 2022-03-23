using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class IntVariable : ScriptableObject, ISerializationCallbackReceiver
{
    [SerializeField] private int InitialeValue;

    [NonSerialized]
    public ReactiveProperty<int> RuntimeValue;

    public void OnAfterDeserialize()
    {
        RuntimeValue = new ReactiveProperty<int>(InitialeValue);
    }

    public void OnBeforeSerialize() { }

    public void OverrideInitialValue(int newValue)
    {
        InitialeValue = newValue;
        RuntimeValue = new ReactiveProperty<int>(InitialeValue);
    }
}
