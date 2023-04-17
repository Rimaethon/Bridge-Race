using System.Collections.Generic;
using Rimaethon.Rimaethon._Scripts.AI_Behavior_System;
using UnityEngine;

namespace Rimaethon._Scripts.AI_Behavior_System
{
    public class BehaviorSystemController : MonoBehaviour
    {
        private BehaviorSystem _system;
        private AIReferences _references;
       
        
       
        private void Start()
        {
            _system = ScriptableObject.CreateInstance<BehaviorSystem>();
            _references = CreateBehaviorSystemReferences();
            _system.Bind(_references);
            var log = ScriptableObject.CreateInstance<DebugLogModule>();
            log.message = "YEY WORKİNG BİTCHES";
            
            var loop = ScriptableObject.CreateInstance<RepeatModule>();
            loop.child = log;
           
            
            _system.rootModule = loop;
            
            if (_system.rootModule==null)
            {
                Debug.Log("system module is null");
            }
        }

        
        AIReferences CreateBehaviorSystemReferences()
        {
            return AIReferences.CreateFromGameObject(gameObject);
        }
        
        
        private void Update()
        {
            Debug.Log("so ım working right ?");
            _system.Update();

        }
    }
}
