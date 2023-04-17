using UnityEngine;

namespace Rimaethon._Scripts.Core
{
    public interface IAnimateAble
    {
        private static readonly int IsRunning = Animator.StringToHash("IsRunning");
        void UpdateAnimation(int animationID,bool animationState);
        void HandleAnimationRequest(int animationHash, bool p1);
    }
}
    