using System.Collections;
using System.Collections.Generic;
using Rimaethon._Scripts.AI_Behavior_System.Runtime;
using UnityEngine;

[System.Serializable]
public class Breakpoint : ActionNode
{
    protected override void OnStart() {
        Debug.Log("Trigging Breakpoint");
        Debug.Break();
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        return State.Success;
    }
}
