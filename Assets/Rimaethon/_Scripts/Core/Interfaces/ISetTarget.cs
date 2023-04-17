using Rimaethon._Scripts.Core.Enums;
using Rimaethon._Scripts.Managers;
using UnityEngine;

namespace Rimaethon._Scripts.Core.Interfaces
{
    public interface ISetTarget
    {
        Vector3 SetTarget(Vector3 currentTransform,PlatformStates platform=PlatformStates.Default,ColorEnum colorEnum=ColorEnum.Default);
    }
}