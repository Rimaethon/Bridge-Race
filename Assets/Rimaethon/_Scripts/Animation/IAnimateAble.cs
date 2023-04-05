using UnityEngine;

namespace _Scripts.Core
{
    public interface IAnimateAble
    {
        private static readonly int IsRunning = Animator.StringToHash("IsRunning");
        void UpdateAnimation(int animationID,bool animationState);
    }
}
    