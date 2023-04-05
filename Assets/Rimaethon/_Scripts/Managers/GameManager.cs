using System;
using System.Collections.Generic;
using Rimaethon._Scripts.Core;
using UnityEngine;
using UnityEngine.Serialization;

namespace Rimaethon._Scripts.Managers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;
    
        public GameStates State;

        public static List<Transform>[] AITargets = new List<Transform>[3];
        private IEventManager _eventManager;



        private void OnEnable()
        {
           
        }

        private void Awake()
        {
            

            instance = this;





        }

        private void Start()
        {
          
        }


        public void UpdateGameState(GameStates newState)
        {
            State = newState;

            switch (newState)
            {
                case GameStates.OnGameStart:

                    _eventManager.Broadcast(GameStates.OnGameStart);
                    
                    break;
            }
        }

        private Action<object[]> HandleGameStart()
        {
            Debug.Log("yes Im working bitches");

            return null;
        }

        

    }
}

