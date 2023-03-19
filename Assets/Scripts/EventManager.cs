using System;
using System.Collections.Generic;
using UnityEngine;

public static class EventManager
{
    private static Dictionary<GameEvent, Action> _eventDictionary
        = new Dictionary<GameEvent, Action>();

    private static Dictionary<GameEvent, Action<object>> _eventDictionaryWithParams
        = new Dictionary<GameEvent, Action<object>>();

    private static Dictionary<GameEvent, Action<object, object>> _eventDictionaryWithTwoParams
        = new Dictionary<GameEvent, Action<object, object>>();

    public static void AddHandler(GameEvent gameEvent, Action action)
    {
        if (!_eventDictionary.ContainsKey(gameEvent))
        {
            _eventDictionary[gameEvent] = action;
        }
        else
        {
            _eventDictionary[gameEvent] += action;
        }
    }

    public static void AddHandler<T>(GameEvent gameEvent, Action<T> action)
    {
        if (!_eventDictionaryWithParams.ContainsKey(gameEvent))
        {
            _eventDictionaryWithParams[gameEvent] = (arg) => { action((T)arg); };
        }
        else
        {
            _eventDictionaryWithParams[gameEvent] += (arg) => { action((T)arg); };
        }
    }

    public static void AddHandler<T1, T2>(GameEvent gameEvent, Action<T1, T2> action)
    {
        if (!_eventDictionaryWithTwoParams.ContainsKey(gameEvent))
        {
            _eventDictionaryWithTwoParams[gameEvent] = (arg1, arg2) => { action((T1)arg1, (T2)arg2); };
        }
        else
        {
            _eventDictionaryWithTwoParams[gameEvent] += (arg1, arg2) => { action((T1)arg1, (T2)arg2); };
        }
    }

    public static void RemoveHandler(GameEvent gameEvent, Action action)
    {
        if (_eventDictionary.ContainsKey(gameEvent))
        {
            _eventDictionary[gameEvent] -= action;
            if (_eventDictionary[gameEvent] == null)
                _eventDictionary.Remove(gameEvent);
        }
    }

    public static void RemoveHandler<T>(GameEvent gameEvent, Action<T> action)
    {
        if (_eventDictionaryWithParams.ContainsKey(gameEvent))
        {
            _eventDictionaryWithParams[gameEvent] -= (arg) => { action((T)arg); };
            if (_eventDictionaryWithParams[gameEvent] == null)
                _eventDictionaryWithParams.Remove(gameEvent);
        }
    }

    public static void RemoveHandler<T1, T2>(GameEvent gameEvent, Action<T1, T2> action)
    {
        if (_eventDictionaryWithTwoParams.ContainsKey(gameEvent))
        {
            _eventDictionaryWithTwoParams[gameEvent] -= (arg1, arg2) => { action((T1)arg1, (T2)arg2); };
            if (_eventDictionaryWithTwoParams[gameEvent] == null)
                _eventDictionaryWithTwoParams.Remove(gameEvent);
        }
    }

    public static void Broadcast(GameEvent gameEvent)
    {
        if (_eventDictionary.ContainsKey(gameEvent))
        {
            _eventDictionary[gameEvent]();
            Debug.Log($"Broadcasting event {gameEvent} with no arguments");
        }

        if (_eventDictionaryWithParams.ContainsKey(gameEvent))
        {
            _eventDictionaryWithParams[gameEvent](null);
            Debug.Log($"Broadcasting event {gameEvent} with null argument");
        }

        if (_eventDictionaryWithTwoParams.ContainsKey(gameEvent))
        {
            _eventDictionaryWithTwoParams[gameEvent](null, null);
            Debug.Log($"Broadcasting event {gameEvent} with null arguments");
        }
    }

    public static void Broadcast<T>(GameEvent gameEvent, T arg)
    {
        if (_eventDictionary.ContainsKey(gameEvent))
        {
            _eventDictionary[gameEvent]();
            Debug.Log($"Broadcasting event {gameEvent} with argument {arg}");
        }

        if (_eventDictionaryWithParams.ContainsKey(gameEvent))
        {
            _eventDictionaryWithParams[gameEvent](arg);
            Debug.Log($"Broadcasting event {gameEvent} with argument {arg}");
        }

        if (_eventDictionaryWithTwoParams.ContainsKey(gameEvent))
        {
            _eventDictionaryWithTwoParams[gameEvent](arg, null);
            Debug.Log($"Broadcasting event {gameEvent} with argument {arg} and null argument");
        }
    }

    public static void Broadcast<T, U>(GameEvent gameEvent, T arg1, U arg2)
    {
        if (_eventDictionaryWithTwoParams.ContainsKey(gameEvent))
        {
            _eventDictionaryWithTwoParams[gameEvent](arg1, arg2);
            Debug.Log($"Broadcasting event {gameEvent} with arguments {arg1}, {arg2}");
        }
    }

}
