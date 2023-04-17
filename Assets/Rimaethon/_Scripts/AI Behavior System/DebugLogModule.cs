using Rimaethon._Scripts.AI_Behavior_System;
using UnityEngine;

namespace Rimaethon.Rimaethon._Scripts.AI_Behavior_System
{
    public class DebugLogModule : ActionModule
    {
        public string message;
        protected override void OnEnter()
        {
            Debug.Log($"OnEnterModule{message}");
                
        }

        protected override void OnExit()
        {
            Debug.Log($"OnExitModule{message}");
        }

        protected override ModuleState OnUpdate()
        {
            Debug.Log($"OnUpdateModule{message}");
            return ModuleState.Completed;
        }
    }
}
