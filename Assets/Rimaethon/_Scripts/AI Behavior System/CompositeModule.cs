using System.Collections.Generic;
using Rimaethon._Scripts.AI_Behavior_System;

namespace Rimaethon.Rimaethon._Scripts.AI_Behavior_System
{
    public abstract class CompositeModule : Module
    {
        public List<Module> children = new List<Module>();
    }
}
