using Rimaethon._Scripts.Managers;
using UnityEngine;

namespace Rimaethon._Scripts.Core
{
    public interface IPlatformAble
    {
        public enum PlatformStates
        {
        
            StartingPlatform,Platform1,Platform2,Platform3,Platform4
        
    
        }
        PlatformStates PlatformState { get;  }
    }
}
