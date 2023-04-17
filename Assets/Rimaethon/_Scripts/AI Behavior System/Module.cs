using Rimaethon.Rimaethon._Scripts.AI_Behavior_System;
using UnityEngine;

namespace Rimaethon._Scripts.AI_Behavior_System
{
    public abstract class Module : ScriptableObject
    {
        public enum ModuleState
        {
            Executing,
            Failed,
            Completed
        }

        
        public ModuleState moduleState = ModuleState.Executing;
        public bool moduleStarted = false;
        public bool drawGizmos = false;
        [HideInInspector] public AIReferences References;
        [HideInInspector] public CommonDataHolder CommonDataHolder;

        public ModuleState Update()
        {
            if (!moduleStarted)
            {
                OnEnter();
                moduleStarted = true;
            }

            moduleState = OnUpdate();
            
            
            if (moduleState is ModuleState.Failed or ModuleState.Completed)
            {
                OnExit();
                moduleStarted = false;
            }

            return moduleState;
        }
        
        

        protected abstract void OnEnter();
        protected abstract void OnExit();
        protected abstract ModuleState OnUpdate();
    }
}
