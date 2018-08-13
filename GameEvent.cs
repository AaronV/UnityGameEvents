using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum GameEventType { None, Float, Int, String, Object, GameType }

[CreateAssetMenu]
public class GameEvent : ScriptableObject
{
    public GameEventType eventType;

    readonly List<GameEventListener> eventListeners =
        new List<GameEventListener>();

    public void Raise()
    {
        _Raise();
    }

    public void Raise(float _f)
    {
        _Raise(_f);
    }

    public void Raise(int _i)
    {
        _Raise(_i);
    }

    public void Raise(object _o)
    {
        _Raise(_o);
    }

    public void Raise(GameType gameType)
    {
        _Raise(gameType);
    }

    void _Raise(object obj = null)
    {
        Debug.Log("GameEvent `" + this.name + "` raised (" + eventType + ") with data: " + obj, this);

        for (int i = eventListeners.Count - 1; i >= 0; i--)
            eventListeners[i].OnEventRaised(obj);
    }

    public void RegisterListener(GameEventListener listener)
    {
        if (!eventListeners.Contains(listener))
            eventListeners.Add(listener);
    }

    public void UnregisterListener(GameEventListener listener)
    {
        if (eventListeners.Contains(listener))
            eventListeners.Remove(listener);
    }
}

