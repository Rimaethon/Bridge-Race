using System.Collections.Generic;
using Rimaethon._Scripts.Core;
using Rimaethon._Scripts.Core.Enums;
using UnityEngine;

namespace Rimaethon._Scripts.Managers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;
    
        public GameStates State;

        public static List<Transform>[] AITargets = new List<Transform>[3];



        
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
        }

        private void Start()
        {
            EventManager.Instance.Broadcast(GameStates.OnObjectsInstantiated, PlatformStates.StartingPlatform);
        }


        public void UpdateGameState(GameStates newState)
        {
            State = newState;
            
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

