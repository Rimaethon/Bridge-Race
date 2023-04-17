using Rimaethon._Scripts.Core;
using Rimaethon._Scripts.Core.Enums;
using Rimaethon._Scripts.Managers;
using Rimaethon.Rimaethon._Scripts.AI_Behavior_System;
using UnityEngine;

namespace Rimaethon._Scripts.AI_Behavior_System
{
    public class GoToDoorState : ActionModule
    {
        public float tolerance = 1.0f;

        
        protected override void OnEnter()
        {
         
            
        }

        protected override void OnExit()
        {
            throw new System.NotImplementedException();
        }

        protected override ModuleState OnUpdate()
        {
            Debug.Log(References.Transform.position);
           // CommonDataHolder.PositionToMove=SetTarget(References.Transform.position, References.CharacterPlatform.PlatformState, References.CharacterType.ColorType);
            if (References.Agent.pathPending) {
                return ModuleState.Executing;
            }

            if (References.Agent.remainingDistance < tolerance) {
                return ModuleState.Completed;
            }

            if (References.Agent.pathStatus == UnityEngine.AI.NavMeshPathStatus.PathInvalid) {
                return ModuleState.Failed;
            }


            

            

            return ModuleState.Executing;
        }
        
        
        
        public Vector3 SetTarget(Vector3 currentTransform, PlatformStates platform = PlatformStates.Default,
            ColorEnum colorEnum = ColorEnum.Default)
        {
            Vector3 currentTarget;
            Vector3 randomDoorPosition =Helpers.PickRandomFromList(SceneDataHolder.DoorsOnPlatforms[platform]).transform.position;
            currentTarget = randomDoorPosition;
            Debug.Log("Okay ım working and ı will return"+ currentTarget);
            return currentTarget;

        }
    }
    
   
}
