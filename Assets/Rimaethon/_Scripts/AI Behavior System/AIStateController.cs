using Rimaethon._Scripts.Core;
using Rimaethon._Scripts.Core.Enums;
using Rimaethon._Scripts.Core.Interfaces;
using Rimaethon._Scripts.Core.Scriptable_Objects;
using Rimaethon._Scripts.Managers;
using Rimaethon.Rimaethon._Scripts.AI_Behavior_System;
using UnityEngine;
using UnityEngine.AI;

namespace Rimaethon._Scripts.Movement.AI_Movement
{
    public class AIStateController : MonoBehaviour
    {
        public AIState currentState;
        public NavMeshAgent navMeshAgent;
        public ColorEnum characterType;
        public PlatformStates characterPlatform;
        private IBrickCountProvider _brickCountProvider;
        private int _brickCount;
        private int _maxBrickCount;
        private AIState _collectState;
        private AIState _goToDoorState;


        // Start is called before the first frame update

        private void OnEnable()
        {
            
        }

        private void Awake()
        {
            _collectState = ScriptableObject.CreateInstance<CollectState>();
            
        }

        void Start()
        {
            _maxBrickCount = Helpers.GiveRandomNumber(7);
            navMeshAgent = GetComponent<NavMeshAgent>();
            characterType = GetComponent<ITypeDeterminer>().ColorType;
            _brickCountProvider = GetComponent<IBrickCountProvider>();
            characterPlatform = GetComponent<IPlatformAble>().PlatformState;
            currentState = _collectState;
        }

        // Update is called once per frame
        void Update()
        {
            DetermineAIBehavior();
            // If the current state is null, return
            if (currentState == null)
                return;

            // Call the current state's Update function
            currentState.UpdateState(this);
        }

        // Call this function to change the AI's state
        public void ChangeState(AIState newState)
        {
            
            // Call the Exit function of the current state
            currentState.ExitState(this);
            // Set the new state
            currentState = newState;
            // Call the Enter function of the new state
            currentState.EnterState(this);
        }

        
        private void DetermineAIBehavior()
        {
            _brickCount = _brickCountProvider.BrickCount;
            Debug.Log("aı has "+_brickCount+" bricks and it is lower than "+_maxBrickCount);
            if (_brickCount>_maxBrickCount)
            {
                
                ChangeState(_goToDoorState);
                _maxBrickCount = Helpers.GiveRandomNumber(7);
            }


            if (_brickCount == 0)
            {
                ChangeState(_collectState);
            }

        }
        
    }
}