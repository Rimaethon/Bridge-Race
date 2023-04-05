using DG.Tweening;
using Rimaethon._Scripts.Core;
using UnityEngine;

namespace Rimaethon._Scripts.Animation
{
    public class DoorOpener : MonoBehaviour,IPlatformAble
    {
        [SerializeField] private IPlatformAble.PlatformStates platformStates;
        public IPlatformAble.PlatformStates PlatformState => platformStates;
        private Transform _leftDoorTransform;
        private Transform _rightDoorTransform;
        private void Start()
        {
            _leftDoorTransform = gameObject.transform.Find("Left Door");
            _rightDoorTransform = gameObject.transform.Find("Right Door");
        }
    
    

        public void OpenDoors()
        {
            _rightDoorTransform.DOMoveX(2.25f, 2);
            _leftDoorTransform.DOMoveX(-2.25f, 2);
        }

        
    }
}
