using Rimaethon._Scripts.Managers;
using UnityEngine;
using UnityEngine.Serialization;

namespace Rimaethon._Scripts.Core
{
    public abstract class Character : MonoBehaviour,ITypeDeterminer,IPlatformAble
    {
   
        [SerializeField] private ITypeDeterminer.ColorEnum colorType;
        public ITypeDeterminer.ColorEnum ColorType => colorType;

        [SerializeField] private IPlatformAble.PlatformStates platformStates;
        public IPlatformAble.PlatformStates PlatformState => platformStates;
    }
}
