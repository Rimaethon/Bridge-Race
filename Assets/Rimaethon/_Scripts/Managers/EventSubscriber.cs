using System;
using Rimaethon._Scripts.Core;

namespace Rimaethon._Scripts.Managers
{
    public static class EventSubscriber
    {
        
        public static void Subscribe(IEventManager eventManager, GameStates gameStates, Action action)
        {
            eventManager.AddHandler(gameStates, action);
        }
        public static void Subscribe<T>(IEventManager eventManager, GameStates gameStates, Action<T> action) 
        {
            eventManager.AddHandler(gameStates, action);
        }
        public static void Subscribe<T1, T2>(IEventManager eventManager, GameStates gameStates, Action<T1,T2> action)
        {
            eventManager.AddHandler(gameStates, action);
        }

        public static void Unsubscribe(IEventManager eventManager, GameStates gameStates, Action action)
        {
            eventManager.RemoveHandler(gameStates, action);
        }
        public static void Unsubscribe<T>(IEventManager eventManager, GameStates gameStates, Action<T> action)
        {
            eventManager.RemoveHandler(gameStates, action);
        }

        public static void Unsubscribe<T1, T2>(IEventManager eventManager, GameStates gameStates, Action<T1,T2> action)
        {
            eventManager.RemoveHandler(gameStates, action);
        }
    }
}