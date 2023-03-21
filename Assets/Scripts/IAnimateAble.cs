using UnityEngine;
using System;
public interface IAnimateAble
{
    Animator Animator { get; set; }
    void Animate();
}
    