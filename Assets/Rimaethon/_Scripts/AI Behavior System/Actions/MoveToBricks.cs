using Rimaethon._Scripts.AI_Behavior_System.Runtime;
using Rimaethon._Scripts.Core;
using Rimaethon._Scripts.Core.Enums;
using UnityEngine;

namespace Rimaethon._Scripts.AI_Behavior_System.Actions
{
    public class MoveToBricks : ActionNode
    {
       

        protected override void OnStart()
        {
            
        }

        protected override void OnStop()
        {
        }

        protected override State OnUpdate()
        {
            
            if (SceneDataHolder.PooledBrickDictionary[PooledObjectStatus.Active][
                    context.characterType.ColorType].Count==0)
            {
                
                
                return State.Success;
                
                
            }
            if (context.brickStacker.BrickCount == 0 && context.IsGoingToDoor )
            {
                blackboard.moveToPosition=Helpers.PickRandomFromList(
                    SceneDataHolder.PooledBrickDictionary[PooledObjectStatus.Active][
                            context.characterType.ColorType]).transform.position;
                    context.IsGoingToDoor = false;
                return State.Success;
            }

            if (context.IsGoingToDoor || context.agent.hasPath) return State.Running;
            blackboard.moveToPosition=Helpers.PickRandomFromList(
                SceneDataHolder.PooledBrickDictionary[PooledObjectStatus.Active][
                    context.characterType.ColorType]).transform.position;
            return State.Success;

        }
    }
}
