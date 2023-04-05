using Rimaethon._Scripts.Core;

namespace Rimaethon._Scripts.Managers
{
    public static class EventBroadcaster
    {

        public static void Broadcast(IEventManager eventManager, GameStates gameStates)
        {
            eventManager.Broadcast(gameStates);
        }
        public static void Broadcast<T>(IEventManager eventManager, GameStates gameStates, T arg)
        {
            eventManager.Broadcast(gameStates, arg);
        }

        public static void Broadcast<T1, T2>(IEventManager eventManager, GameStates gameStates, T1 arg1, T2 arg2)
        {
            eventManager.Broadcast(gameStates, arg1, arg2);
        }
    }
}