using System.Collections.Generic;
using DG.Tweening;
using Rimaethon._Scripts.Core;
using Rimaethon._Scripts.Core.Interfaces;
using Rimaethon._Scripts.Managers;
using UnityEngine;

namespace Rimaethon._Scripts.Animation
{
    public class Door : MonoBehaviour,IPlatformAble
    {
        [SerializeField] private PlatformStates platformState;
        public PlatformStates PlatformState => platformState;
        [SerializeField] private Transform _leftDoorTransform;
        [SerializeField] private Transform _rightDoorTransform;

        
        private void Awake()
        {
            if (SceneDataHolder._doorsOnPlatforms == null)
            {
                SceneDataHolder._doorsOnPlatforms = new Dictionary<PlatformStates, List<GameObject>>();
            }

            if (!SceneDataHolder._doorsOnPlatforms.ContainsKey(platformState))
            {
                SceneDataHolder._doorsOnPlatforms.Add(platformState, new List<GameObject>());
            }

            SceneDataHolder._doorsOnPlatforms[platformState].Add(gameObject);
        }

    


        private void OnTriggerEnter(Collider other)
        {

            if (!other.gameObject.CompareTag("Character")|| other.gameObject.GetComponent<Character>().PlatformState!=platformState) return;
            OpenDoors();
            EventManager.Instance.Broadcast<GameObject>(GameStates.OnCharacterLevelChange,other.gameObject);
        }

        private void OpenDoors()
        {
            float rightDoorTargetX = _rightDoorTransform.position.x + 1.5f;
            float leftDoorTargetX = _leftDoorTransform.position.x - 1.5f;
            Debug.Log("I opened door");
            _rightDoorTransform.DOMoveX(rightDoorTargetX , 2);
            _leftDoorTransform.DOMoveX(leftDoorTargetX, 2);
        }

        
    }
}
