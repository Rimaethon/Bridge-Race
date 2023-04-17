using Rimaethon._Scripts.Core;
using Rimaethon._Scripts.Core.Enums;
using Rimaethon._Scripts.Core.Interfaces;
using Rimaethon._Scripts.Core.Scriptable_Objects;
using Rimaethon._Scripts.Managers;
using UnityEngine;
using UnityEngine.AI;

namespace Rimaethon._Scripts.Movement.AI_Movement
{
    public class CollectState : AIState
    {
        // Actions to take when entering this state
        public override void EnterState(AIStateController controller)
        {
            controller.navMeshAgent.SetDestination(SetTarget(controller.transform.position, controller.characterPlatform, controller.characterType));
        }

        // Actions to take while in this state
        public override void UpdateState(AIStateController controller)
        {
            // If the AI has reached its destination, set the next destination
            if (!controller.navMeshAgent.pathPending && controller.navMeshAgent.remainingDistance <= controller.navMeshAgent.stoppingDistance)
            {
                controller.navMeshAgent.SetDestination(SetTarget(controller.transform.position, controller.characterPlatform, controller.characterType));
            }
        }

        // Actions to take when exiting this state
        public override void ExitState(AIStateController controller)
        {
            // Stop the AI's movement
            controller.navMeshAgent.ResetPath();
        }
        
        public Vector3 SetTarget(Vector3 currentTransform, PlatformStates platform = PlatformStates.Default, ColorEnum characterType = ColorEnum.Default)
        {
            int maxTries = 3;
            int numTries = 0;

            while (numTries < maxTries)
            {
                Vector3 randomTarget = Helpers.PickRandomFromList(SceneDataHolder.PooledBrickDictionary[BrickStatus.PooledBrickStatus.Active][characterType]).transform.position;

                if (Vector3.Distance(currentTransform, randomTarget) >= 4f)
                {
                    return randomTarget;
                }

                numTries++;
            }

            Debug.Log("Failed to find a suitable target after " + maxTries + " attempts.");
            return currentTransform;
        }
    }
}
