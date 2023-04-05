using System;
using System.Collections.Generic;
using Rimaethon._Scripts.Core;
using Rimaethon._Scripts.Managers;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Rimaethon._Scripts.ObjectManagers
{
    public class PoolSpawner: MonoBehaviour
    {
        private int _brickCountPerColor;
        [SerializeField] private float spawnInterval = 0.3f;
        [SerializeField] private float sphereOverlapRadius = 1.0f;
        private ISceneDataHolder _sceneDataHolder;
        
        private  List<ITypeDeterminer.ColorEnum> _availableColors = new List<ITypeDeterminer.ColorEnum>();
        [SerializeField] private float spawnTimer = 0f;
        private Dictionary<IPlatformAble.PlatformStates, List<Vector3>> _brickSpawnPointsOfPlatforms;
        private ObjectPool _objectPool;
        private List<Vector3> _spawnPoints;
        private bool _isGameStart;
        private GameObject _brick;
        private ITypeDeterminer.ColorEnum _randomColor;
        

        private void OnEnable()
        {
            EventSubscriber.Subscribe<IPlatformAble.PlatformStates>(EventManager.Instance,GameStates.OnObjectsInstantiated,SpawnBricksAtPlatforms);

        }

        private void OnDisable()
        {
            EventSubscriber.Unsubscribe<IPlatformAble.PlatformStates>(EventManager.Instance,GameStates.OnObjectsInstantiated,SpawnBricksAtPlatforms);
        }

        private void Awake()
        {

            _brickSpawnPointsOfPlatforms = GetComponent<TextFilePositionExtractor>().GetBrickSpawnPoints();
            _objectPool = GetComponent<ObjectPool>();
            
        }



        private void SpawnBricksAtPlatforms(IPlatformAble.PlatformStates platform)
        {
            _availableColors = SceneDataHolder.CharactersTypesOnLevels[IPlatformAble.PlatformStates.StartingPlatform];
            _spawnPoints = _brickSpawnPointsOfPlatforms[platform];
            for (int i = 0; i <_spawnPoints.Count-1 ; i++)
            {
                _randomColor = Helpers.PickRandomFromList(_availableColors);
                _brick = _objectPool.GetBrickFromPool(_randomColor);
                _brick.transform.position = _spawnPoints[i];
                _brick.SetActive(true);

            }
            
            

        }



       


        void RespawnCollectedBricks()
        {
            _brick.transform.position = AssignNewSpawnPoint(IPlatformAble.PlatformStates.StartingPlatform);
            
        }
        
        
        private Vector3 AssignNewSpawnPoint(IPlatformAble.PlatformStates platform)
        {
            _spawnPoints = _brickSpawnPointsOfPlatforms[platform];
            
            
            Vector3 newSpawnPoint = Helpers.PickRandomFromList(_spawnPoints);

            Collider[] overlappingColliders = Physics.OverlapSphere(newSpawnPoint, sphereOverlapRadius);

            foreach (Collider col in overlappingColliders)
            {
                Debug.Log(col.tag);
                if (col.CompareTag("Brick"))
                {
                 
                    newSpawnPoint = _spawnPoints[Random.Range(0, _spawnPoints.Count)];
                    Physics.OverlapSphere(newSpawnPoint, sphereOverlapRadius);
                    Debug.Log("I HAVE SPAWNED SİNCE THİS İS NOT USED BEFORE"+newSpawnPoint);
                    
                }
            }

            return newSpawnPoint;
        }


        
    
    }
}

