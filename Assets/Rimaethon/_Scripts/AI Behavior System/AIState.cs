using Rimaethon._Scripts.Movement.AI_Movement;
using UnityEngine;

namespace Rimaethon._Scripts.Core.Scriptable_Objects
{
    public abstract class AIState : ScriptableObject
    {
        // Actions to take when entering this state
        public abstract void EnterState(AIStateController controller);
        // Actions to take while in this state
        public abstract void UpdateState(AIStateController controller);
        // Actions to take when exiting this state
        public abstract void ExitState(AIStateController controller);
    }
}
