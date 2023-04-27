using UnityEngine;

namespace Rimaethon._Scripts.AI_Behavior_System.Runtime {
    public abstract class DecoratorNode : Node {

        [SerializeReference]
        [HideInInspector] 
        public Node child;
    }
}
