using System.Collections;
using System.Collections.Generic;
using Rimaethon._Scripts.Core;
using Rimaethon._Scripts.Core.Enums;
using Rimaethon._Scripts.Managers;
using UnityEngine;

namespace Rimaethon._Scripts.ObjectManagers
{
    public class PoolSpawner: MonoBehaviour
    {
        private int _brickCountPerColor;
         private float spawnInterval = 3f;
        [SerializeField] private float sphereOverlapRadius = 1.0f;
        
        private  List<ColorEnum> _ColorsInPlatform = new List<ColorEnum>();
        private  List<ColorEnum> _ColorsInGame = new List<ColorEnum>();
        [SerializeField] private float spawnTimer = 0f;
        private ObjectPool _objectPool;
        [SerializeField] private List<Vector3> _spawnPoints;
        private bool _isGameStart;
        private GameObject _brick;
        private ColorEnum _randomColor;
        private int tryCount;
        Vector3 newSpawnPoint;
        [SerializeField]private int spawnedBrickCount;

        private void OnEnable()
        {
            EventManager.Instance.AddHandler<PlatformStates>(GameStates.OnObjectsInstantiated,SpawnBricksAtPlatforms);

        }

        private void OnDisable()
        {

            EventManager.Instance.RemoveHandler<PlatformStates>(GameStates.OnObjectsInstantiated,SpawnBricksAtPlatforms);
            
        }

        private void Awake()
        {

            _objectPool = GetComponent<ObjectPool>();
            _ColorsInGame=SceneDataHolder.CharactersTypesOnLevels[PlatformStates.StartingPlatform];

            
        }

    

        private void SpawnBricksAtPlatforms(PlatformStates platform)
        {
            _spawnPoints = SceneDataHolder._spawnPoints[platform];
            _ColorsInPlatform = SceneDataHolder.CharactersTypesOnLevels[platform];

            int numBricksPerColor = Mathf.FloorToInt((float)_spawnPoints.Count / _ColorsInGame.Count);
            int numColorsWithExtraBrick = _spawnPoints.Count % _ColorsInGame.Count;

            foreach (ColorEnum color in _ColorsInPlatform)
            {
                int numBricksToSpawn = numBricksPerColor;
                if (numColorsWithExtraBrick > 0)
                {
                    numBricksToSpawn++;
                    numColorsWithExtraBrick--;
                    Debug.Log(numColorsWithExtraBrick+" bricks left");
                }

                for (int i = 0; i < numBricksToSpawn; i++)
                {
                    _brick = _objectPool.GetBrickFromPool(color);
                    while (_brick==null&& tryCount<3)
                    {
                        _brick = _objectPool.GetBrickFromPool(color);
                        tryCount++;
                    }


                    if (_brick != null)
                    {
                        Vector3 newSpawnPoint = AssignNewSpawnPoint(platform);
                        _brick.transform.position = newSpawnPoint;
                        _brick.SetActive(true);
                       
                    }

                    tryCount = 0;
                }
            }

            StartCoroutine(SpawnBricksOnInterval(platform));
        }


        
        
        private Vector3 AssignNewSpawnPoint(PlatformStates platform)    
        {

            if (_spawnPoints.Count != 0)
            {
                newSpawnPoint = Helpers.PickRandomFromList(_spawnPoints);
                
                if(! SceneDataHolder.spawnedBrickPositions.ContainsKey(platform))
                {
                    SceneDataHolder.spawnedBrickPositions[platform] = new List<Vector3>();
                }
                SceneDataHolder.spawnedBrickPositions[platform].Add(newSpawnPoint);
                
                _spawnPoints.Remove(newSpawnPoint);
                Debug.Log(_spawnPoints.Count+"spawn point count in AssignNewSpawnPoint");
            }
            

            return newSpawnPoint;
        }

        


        private IEnumerator SpawnBricksOnInterval(PlatformStates platform)
        {
            

            _spawnPoints = SceneDataHolder._spawnPoints[platform];
            while (true)
            {
                ColorEnum color=Helpers.PickRandomFromList(_ColorsInPlatform);
                
                if ( _spawnPoints.Count>0)
                {
                    GameObject brick = _objectPool.GetBrickFromPool(color);
                    if (brick != null)
                    {
                        Debug.Log("I will give a new spawn point since "+_spawnPoints.Count+" has points to give");
                        Vector3 spawnPoint = AssignNewSpawnPoint(platform);
                        brick.transform.position = spawnPoint;
                        brick.SetActive(true);
                        spawnedBrickCount++;
                        Debug.Log("Object interval spawner spawned"+spawnedBrickCount);
                    }
                    
                }
                yield return new WaitForSeconds(spawnInterval);
            }
        }

    
    }
}

