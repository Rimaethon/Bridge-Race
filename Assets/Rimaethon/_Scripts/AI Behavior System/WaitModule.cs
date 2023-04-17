using Rimaethon._Scripts.AI_Behavior_System;
using UnityEngine;

namespace Rimaethon.Rimaethon._Scripts.AI_Behavior_System
{
    public class WaitModule : ActionModule
    {
        public float waitDuration = 1;
        private float _startTime;
        protected override void OnEnter()
        {
            _startTime = Time.time;
        }

        protected override void OnExit()
        {
            
        }

        protected override ModuleState OnUpdate()
        {
            if (Time.time - _startTime > waitDuration)
            {
                return ModuleState.Completed;
            }

            return ModuleState.Executing;
        }
    }
}
