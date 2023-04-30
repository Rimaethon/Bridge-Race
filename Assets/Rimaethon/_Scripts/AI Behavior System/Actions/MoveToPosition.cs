using Rimaethon._Scripts.AI_Behavior_System.Runtime;
using UnityEngine;

namespace Rimaethon._Scripts.AI_Behavior_System.Actions
{
    public class MoveToPosition : ActionNode {
        public float speed = 5;
        public float stoppingDistance = 0.1f;
        public bool updateRotation = true;
        public float acceleration = 40.0f;
        public float tolerance = 1.0f;

        

        protected override void OnStart()
        {
            context.agent.stoppingDistance = stoppingDistance;
            context.agent.speed = speed;
            context.agent.updateRotation = updateRotation;
            context.agent.acceleration = acceleration;
        }

        protected override void OnStop()
        {
        }

        protected override State OnUpdate()
        {
            context.agent.destination = blackboard.moveToPosition;
            if (context.brickStacker.BrickCount is 0 or > 4)
            {
                return State.Success;
            }
            if (context.agent.pathPending)
            {
                Debug.Log("I need to go" + blackboard.moveToPosition + "since " +
                          Vector3.Distance(context.transform.position, blackboard.moveToPosition));
                return State.Running;
            }

            if (context.agent.remainingDistance < tolerance) {
                return State.Success;
            }

            if (context.agent.pathStatus == UnityEngine.AI.NavMeshPathStatus.PathInvalid) {
                return State.Failure;
            }
            return State.Running;
        }
    }
}
