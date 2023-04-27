using Rimaethon._Scripts.AI_Behavior_System.Runtime;
using UnityEngine;

namespace Rimaethon._Scripts.AI_Behavior_System.Composites
{
    [System.Serializable]
    public class ChooseTarget : CompositeNode {
 

        protected override void OnStart() {
        }

        protected override void OnStop() {
        }

        protected override State OnUpdate()
        {
            Debug.Log("Yeah Im always choosing target");
            if (context.brickStacker.BrickCount > 4)
            {
                Debug.Log("Go to door working");
                children[0].Update();
                
            }
            else
            {
                Debug.Log("move to bricks working");
                children[1].Update();

            }
            return State.Success;
        }
    }
}
