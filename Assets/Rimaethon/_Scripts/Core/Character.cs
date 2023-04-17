using Rimaethon._Scripts.Core.Enums;
using Rimaethon._Scripts.Core.Interfaces;
using Rimaethon._Scripts.Managers;
using UnityEngine;
using UnityEngine.Serialization;

namespace Rimaethon._Scripts.Core
{
    public abstract class Character : MonoBehaviour,ITypeDeterminer,IPlatformAble
    {
   
        [SerializeField] private ColorEnum colorType;
        public ColorEnum ColorType => colorType;

        [SerializeField] private PlatformStates platformStates;
        public PlatformStates PlatformState => platformStates;
    }
}
