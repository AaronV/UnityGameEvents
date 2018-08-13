using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class UnityEventObject : UnityEvent<object> { }

public class GameEventListener : MonoBehaviour
{
    [Tooltip("Event to register with.")]
    public GameEvent Event;

    [Tooltip("Response to invoke when Event is raised.")]
    public UnityEventObject Response;

    private void OnEnable()
    {
        Event.RegisterListener(this);
    }

    private void OnDisable()
    {
        Event.UnregisterListener(this);
    }

    public void OnEventRaised(object _o)
    {
        if (_o == null) _o = new object();
        Response.Invoke(_o);
    }
}