using System.Collections.Generic;
using UnityEngine;

namespace Rimaethon._Scripts.AI_Behavior_System.Runtime {

    [System.Serializable]
    public abstract class CompositeNode : Node {

        [HideInInspector] 
        [SerializeReference]
        public List<Node> children = new List<Node>();
    }
}