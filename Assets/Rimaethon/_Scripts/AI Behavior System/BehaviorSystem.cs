using System.Collections.Generic;
using Rimaethon.Rimaethon._Scripts.AI_Behavior_System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Rimaethon._Scripts.AI_Behavior_System
{
    [CreateAssetMenu()]
    public class BehaviorSystem : ScriptableObject
    { 
        [SerializeReference]
        public Module rootModule;
        
        [SerializeReference]
        public Module.ModuleState systemState = Module.ModuleState.Executing;
        
        public CommonDataHolder CommonDataHolder = new CommonDataHolder();

        public List<Module> modules = new List<Module>();

        
        
        public Module.ModuleState Update()
        {
            

            
            return rootModule.Update();
        }
        
        
        public void Bind(AIReferences reference) 
        {
            
                rootModule.References = reference;
                rootModule.CommonDataHolder = CommonDataHolder;
                Debug.Log("context and blackboard binded");
        }
        
        
      





    }
}
