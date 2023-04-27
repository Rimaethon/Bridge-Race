using System.Collections.Generic;
using Rimaethon._Scripts.AI_Behavior_System.Runtime;
using Rimaethon._Scripts.Core;
using Rimaethon._Scripts.Core.Enums;
using Rimaethon._Scripts.Managers;
using UnityEngine;

namespace Rimaethon._Scripts.AI_Behavior_System
{
    public class GoToDoorState : ActionNode
    {
        private float tolerance = 1.0f;

        protected override void OnStart()
        {
        }

        protected override void OnStop()
        {
        }

        protected override State OnUpdate()
        { 
            
            if (Vector3.Distance(context.transform.position, blackboard.moveToPosition) < tolerance ||
              context.brickStacker.BrickCount < 4)return State.Success;
            
            blackboard.moveToPosition = SetTarget(context.transform.position,
                context.characterPlatform.PlatformState, context.characterType.ColorType);
            
            context.IsGoingToDoor = true;
           
            return State.Running;
        }
        
        
        
        public Vector3 SetTarget(Vector3 currentTransform, PlatformStates platform = PlatformStates.Default, ColorEnum colorEnum = ColorEnum.Default)
        {
            // Get the list of doors for the current platform state
            List<GameObject> doorsOnPlatform = SceneDataHolder._doorsOnPlatforms[context.characterPlatform.PlatformState];
    
            // Find the nearest door to the current transform
            float minDistance = Mathf.Infinity;
            Vector3 nearestDoorPosition = Vector3.zero;
            foreach (GameObject door in doorsOnPlatform)
            {
                float distance = Vector3.Distance(currentTransform, door.transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearestDoorPosition = door.transform.position;
                }
            }

            // Get a random stair from the nearest door
            var currentTarget = nearestDoorPosition;
    
            return currentTarget;
        }

    }
    
   
}
