using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

[CreateAssetMenu]
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
}
