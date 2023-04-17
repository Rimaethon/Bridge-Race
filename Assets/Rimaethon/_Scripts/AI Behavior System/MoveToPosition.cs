using UnityEngine;

namespace Rimaethon._Scripts.AI_Behavior_System
{
    public class MoveToPosition : ActionModule {
        public float speed = 5;
        public float stoppingDistance = 0.1f;
        public bool updateRotation = true;
        public float acceleration = 40.0f;
        public float tolerance = 1.0f;

        protected override void OnEnter()
        {
            References.Agent.stoppingDistance = stoppingDistance;
            References.Agent.speed = speed;
            References.Agent.destination = CommonDataHolder.PositionToMove;
            References.Agent.updateRotation = updateRotation;
            References.Agent.acceleration = acceleration;
        }
        
        protected override void OnExit()
        {
        }

        protected override ModuleState OnUpdate() {
            if (References.Agent.pathPending) {
                return ModuleState.Executing;
            }

            if (References.Agent.remainingDistance < tolerance) {
                return ModuleState.Completed;
            }

            if (References.Agent.pathStatus == UnityEngine.AI.NavMeshPathStatus.PathInvalid) {
                return ModuleState.Failed;
            }
            Debug.Log("Im trying to move a position");
            return ModuleState.Executing;
        }
    }
}
