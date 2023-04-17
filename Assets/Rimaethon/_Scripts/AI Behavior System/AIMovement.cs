using System.Collections.Generic;
using Rimaethon._Scripts.Core;
using Rimaethon._Scripts.Core.Enums;
using Rimaethon._Scripts.Core.Interfaces;
using Rimaethon._Scripts.Managers;
using UnityEngine;
using UnityEngine.AI;

namespace Rimaethon.Rimaethon._Scripts.AI_Behavior_System
{
    public class AIMovement : MonoBehaviour, IHaveAIStates
    {
        [SerializeField] private List<Transform> targetPoints = new List<Transform>();
        [SerializeField] private float moveSpeed = 5f;
        [SerializeField] private float rotationSpeed = 3f;
        private const float TargetRadius = 2f;
        private NavMeshAgent _navMeshAgent;
        private ColorEnum _characterType;
        private AIStates _AIState;
        private ISetTarget _behaviour;
        private Vector3 _lastTargetPosition;
        private PlatformStates _platformState;

        private void Awake()
        {
            _characterType = GetComponent<Character>().ColorType;
            _behaviour = GetComponent<ISetTarget>();
            _AIState = GetComponent<AI>().currentAIState;
            _platformState = GetComponent<IPlatformAble>().PlatformState;
        }

        private void Start()
        {
           
        }

        private void Update()
        {

          
        }

       

        public void SetBehavior(ISetTarget strategy)
        {
            _behaviour = strategy;
            Debug.Log("Behavior is set to"+strategy);
            
        }
    }
}
