using UnityEngine;
using System;
public interface IAnimateAble
{
    Animator Animator { get; set; }
    
    private static readonly int IsWinning = Animator.StringToHash("IsWinning");
    private static readonly int IsRunning = Animator.StringToHash("IsRunning");

    void Animate();
}
    