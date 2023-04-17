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
            base.HandleAnimationRequest(animationID, animationState);
            if (_isWinning)
            {
                return;
            }
            
            
            
            // if (animationID == CharacterAnimation.IsRunning && animationState == true)
            // {
            //     _isWinning = true;
            // }
            
            
        }

        
    }
}
