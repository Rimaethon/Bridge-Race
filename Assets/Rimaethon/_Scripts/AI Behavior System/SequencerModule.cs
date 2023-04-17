using Unity.VisualScripting;

namespace Rimaethon.Rimaethon._Scripts.AI_Behavior_System
{
    public class SequencerModule : CompositeModule
    {
        private int currentChild;
        protected override void OnEnter()
        {
            currentChild = 0;

        }

        protected override void OnExit()
        {
            
        }

        protected override ModuleState OnUpdate()
        {
            var child = children[currentChild];
            switch (child.Update())
            {
                case ModuleState.Executing:
                    return ModuleState.Executing;
                case ModuleState.Failed:
                    return ModuleState.Failed;
                case ModuleState.Completed:
                    currentChild++;
                    break;
            }
            return currentChild==children.Count ? ModuleState.Completed : ModuleState.Executing;
        }
    }
}
