using System.Collections;
using System.Collections.Generic;
using Rimaethon._Scripts.AI_Behavior_System.Runtime;
using Rimaethon._Scripts.Core;
using Rimaethon._Scripts.Core.Enums;
using UnityEditor;
using UnityEngine;

public class HoverState : ActionNode
{
    

    protected override void OnStart()
    {
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
       // Debug.Log("Yeah ım working and " +SceneDataHolder.PooledBrickDictionary[PooledObjectStatus.Active][context.characterType.ColorType].Count);
            
        if (SceneDataHolder.PooledBrickDictionary[PooledObjectStatus.Active][
                context.characterType.ColorType].Count != 0 || context.agent.hasPath) return State.Success;
        
        blackboard.moveToPosition = Helpers.PickRandomFromList(SceneDataHolder.spawnedBrickPositions);
        Debug.Log("There are "+SceneDataHolder.PooledBrickDictionary[PooledObjectStatus.Active][
            context.characterType.ColorType].Count+" active bricks and ı will go to "+ blackboard.moveToPosition);

        return State.Success;

    }
}
