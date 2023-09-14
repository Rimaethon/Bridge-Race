using System.Collections.Generic;
using Rimaethon._Scripts.Core;
using Rimaethon._Scripts.Core.Enums;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Rimaethon._Scripts.Managers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;
        public GameStates State;



        
        private void OnEnable()
        {
            EventManager.Instance.AddHandler<GameObject>(GameStates.OnCharacterLevelChange, SetPlatform);
        }
        private void OnDisable()
        {
            EventManager.Instance.RemoveHandler<GameObject>(GameStates.OnCharacterLevelChange, SetPlatform);
        }
        
        private void Awake()
        {
            instance = this;
            UpdateGameState(GameStates.OnBeforeGameStart);
        }

     


        public void UpdateGameState(GameStates newState)
        {
            State = newState;
            switch (newState)
            {
                case GameStates.OnBeforeGameStart:
                    Debug.Log("I called OnBeforeGameStart");
                    EventManager.Instance.Broadcast<Dictionary<PlatformStates,List<Vector3>>>(GameStates.OnBeforeGameStart,SceneDataHolder._spawnPoints);
                    
                    break;
                case GameStates.OnGameStart:
                    EventManager.Instance.Broadcast(GameStates.OnGameStart);
                    Debug.Log("I called OnGameStart");
                    break;
                case GameStates.OnObjectsInstantiated:
                    EventManager.Instance.Broadcast(GameStates.OnObjectsInstantiated, PlatformStates.StartingPlatform);
                    break;
               
                    break;
            }
        }

        

        private static void SetPlatform(GameObject character)
        {
            PlatformStates existingPlatformState = character.GetComponent<Character>().PlatformState;
            PlatformStates nextPlatformState = existingPlatformState.GetNextState();

    
            character.GetComponent<Character>().PlatformState = nextPlatformState;
            ColorEnum color = character.GetComponent<Character>().ColorType;

            if (SceneDataHolder.CharactersTypesOnLevels.ContainsKey(existingPlatformState))
            {
                SceneDataHolder.CharactersTypesOnLevels[existingPlatformState].Remove(color);
            }

            if (!SceneDataHolder.CharactersTypesOnLevels.ContainsKey(nextPlatformState))
            {
                SceneDataHolder.CharactersTypesOnLevels[nextPlatformState] = new List<ColorEnum>();
            }
            SceneDataHolder.CharactersTypesOnLevels[nextPlatformState].Add(color);
            EventManager.Instance.Broadcast(GameStates.OnObjectsInstantiated, nextPlatformState);
            
        }
      


    }
}

