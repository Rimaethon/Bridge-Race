using System;
using UnityEngine;

namespace _Scripts.Core
{
    public class CharacterAnimation : MonoBehaviour,IAnimateAble
    {
        private Animator _animator;
        public static readonly int IsRunning = Animator.StringToHash("IsRunning");


        private void Awake()
        {
            _animator = GetComponentInChildren<Animator>();
        }

        public CharacterAnimation(Animator animator)
        {
            _animator = animator;
        }

        public void UpdateAnimation(int animationID, bool animationState)
        {
            _animator.SetBool(animationID, animationState);
        }
        
        public virtual void HandleAnimationRequest(int animationID, bool animationState)
        {
            UpdateAnimation(animationID, animationState);
        }

    }
}
