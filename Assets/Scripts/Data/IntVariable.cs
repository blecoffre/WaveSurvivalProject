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
}
