using Rimaethon.Rimaethon._Scripts.AI_Behavior_System;

namespace Rimaethon._Scripts.AI_Behavior_System
{
    public class RepeatModule : DecoratorModule
    {
        protected override void OnEnter()
        {
            
        }

        protected override void OnExit()
        {
            
        }

        protected override ModuleState OnUpdate()
        {
            child.Update(); 
            return ModuleState.Executing;
        }
    }
}
