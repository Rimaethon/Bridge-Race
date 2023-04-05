using _Scripts.Core;
using UnityEngine;

namespace Rimaethon._Scripts.Animation
{
    public class PlayerAnimation : CharacterAnimation
    {
        private bool _isWinning = false;
    
    
        public PlayerAnimation(Animator animator) : base(animator)
        {
        }
        public override void HandleAnimationRequest(int animationID, bool animationState)
        {
            if (_isWinning)
            {
                return;
            }
            
            base.HandleAnimationRequest(animationID, animationState);
            
            // if (animationID == CharacterAnimation.IsRunning && animationState == true)
            // {
            //     _isWinning = true;
            // }
            
            
        }

        
    }
}
