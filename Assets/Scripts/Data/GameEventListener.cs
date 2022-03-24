using System;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class GameEventListener : MonoBehaviour
{
    public GameEvent Event;
    public UnityEvent Response;

    public void OnEventRaised()
    { Response.Invoke(); }

    public void OnEnable()
    {
        Event?.RegisterListener(this);
    }

    public void OnDisable()
    {
        Event?.UnregisterListener(this);
    }
}