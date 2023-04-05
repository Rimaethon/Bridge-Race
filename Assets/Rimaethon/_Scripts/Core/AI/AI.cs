using UnityEngine;
using UnityEngine.Serialization;

namespace Rimaethon._Scripts.Core.AI
{
    public class AI : Character
    {
        public enum AIStates
           {
               Collect,
               GoToDoor,
               AttackPlayer
           }
       
            private AIStates _currentAIStates;
       
           void Update()
           {
               switch (_currentAIStates)
               {
                   case AIStates.Collect:
                       // Execute collect state logic
                       break;
                   case AIStates.GoToDoor:
                       
                       break;
                   case AIStates.AttackPlayer:
                       // Execute attack player state logic
                       break;
                   default:
                       break;
               }
           }

        


    }
}


