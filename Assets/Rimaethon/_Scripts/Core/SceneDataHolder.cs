using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using Rimaethon._Scripts.Core.Enums;
using Rimaethon._Scripts.Core.Interfaces;
using Rimaethon._Scripts.Managers;
using UnityEngine;


namespace Rimaethon._Scripts.Core
{
    public  class SceneDataHolder : MonoBehaviour
    {
        private static Dictionary<PlatformStates, List<ColorEnum>> _characterTypesOnLevels =
            new Dictionary<PlatformStates, List<ColorEnum>>();

        public static Dictionary<BrickStatus.PooledBrickStatus, Dictionary<ColorEnum, List<GameObject>>> PooledBrickDictionary =
            new Dictionary<BrickStatus.PooledBrickStatus, Dictionary<ColorEnum, List<GameObject>>>()
            {
                { BrickStatus.PooledBrickStatus.Active, new Dictionary<ColorEnum, List<GameObject>>() },
                { BrickStatus.PooledBrickStatus.NotActive, new Dictionary<ColorEnum, List<GameObject>>() }
            };
        private static bool _characterTypesInitialized = false;

       private static SerializedDictionary<PlatformStates, List<GameObject>> _doorsOnPlatforms =
            new SerializedDictionary<PlatformStates, List<GameObject>>();
       

       public static SerializedDictionary<PlatformStates, List<GameObject>> DoorsOnPlatforms => _doorsOnPlatforms;

        public static Dictionary<PlatformStates, List<ColorEnum>> CharactersTypesOnLevels
        {
            get
            {
                if (!_characterTypesInitialized)
                {
                    FindCharacterTypesAtStart();
                }

                return _characterTypesOnLevels;
            }
        }

        private static void FindCharacterTypesAtStart()
        {
            _characterTypesOnLevels.Clear();

            foreach (GameObject character in GameObject.FindGameObjectsWithTag("Character"))
            {
                Character characterScript = character.GetComponent<Character>();

                if (characterScript != null)
                {
                    ColorEnum characterColor = characterScript.ColorType;
                    PlatformStates platformState = characterScript.PlatformState;

                    if (!_characterTypesOnLevels.ContainsKey(platformState))
                    {
                        _characterTypesOnLevels.Add(platformState, new List<ColorEnum>());
                    }

                    _characterTypesOnLevels[platformState].Add(characterColor);

                }
            }
            

            _characterTypesInitialized = true;
        }
        
        public static void SetPlatform(Character character, PlatformStates platformState)
        {
            // Get the existing platform of the character
            PlatformStates existingPlatformState = character.PlatformState;

            // Get the color of the character
            ColorEnum color = character.ColorType;

            // Remove the character from the existing platform's dictionary
            if (_characterTypesOnLevels.ContainsKey(existingPlatformState))
            {
                _characterTypesOnLevels[existingPlatformState].Remove(color);
            }

            // Add the character to the new platform's dictionary
            if (_characterTypesOnLevels.ContainsKey(platformState))
            {
                _characterTypesOnLevels[platformState].Add(color);
            }
            else
            {
                Debug.LogError($"Invalid platform state: {platformState}");
            }

            // Update the character's platform state
        }
        
    }
}