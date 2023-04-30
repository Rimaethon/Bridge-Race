using System;
using System.Collections.Generic;
using Rimaethon._Scripts.Core.Enums;
using Rimaethon._Scripts.Managers;
using UnityEngine;
using UnityEngine.Serialization;


namespace Rimaethon._Scripts.Core
{
    public  class SceneDataHolder : MonoBehaviour
    {
        private static Dictionary<PlatformStates, List<ColorEnum>> _characterTypesOnLevels =
            new Dictionary<PlatformStates, List<ColorEnum>>();

        public static Dictionary<PooledObjectStatus, Dictionary<ColorEnum, List<GameObject>>> PooledBrickDictionary =
            new Dictionary<PooledObjectStatus, Dictionary<ColorEnum, List<GameObject>>>()
            {
                { PooledObjectStatus.Active, new Dictionary<ColorEnum, List<GameObject>>() },
                { PooledObjectStatus.OnPlayer, new Dictionary<ColorEnum, List<GameObject>>() },
                { PooledObjectStatus.NotActive, new Dictionary<ColorEnum, List<GameObject>>() }
            };
        private static bool _characterTypesInitialized = false;
        private static bool _spawnPointsInitialized= false;
        
       public static Dictionary<PlatformStates, List<Vector3>> spawnedBrickPositions=new Dictionary<PlatformStates, List<Vector3>>() ;

        public static  Dictionary<PlatformStates, List<Vector3>> _spawnPoints =
            new Dictionary<PlatformStates, List<Vector3>>();


        public static Dictionary<PlatformStates, List<GameObject>> _doorsOnPlatforms;

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
        
    }
}