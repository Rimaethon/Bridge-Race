/*using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class PoolSpawner : MonoBehaviour
{
    [SerializeField] Transform[] spawnPoints; // the positions where objects will be spawned
    private float _spawnInterval = 0.3f; // the time between spawns
    private float _spawnTimer = 0f;
    [SerializeField] ObjectPooler objectPooler;
    private Transform _player;
    private int _brickType;
    private TypeDeterminer _typeDeterminer;
    private List<TypeDeterminer.ColorEnum> _characterTypesInScene;
    public static Dictionary<TypeDeterminer.ColorEnum, List<GameObject>> BrickPools = new Dictionary<TypeDeterminer.ColorEnum, List<GameObject>>();
    // list of active spawned objects
    private void Awake()
    {
        FindCharactersInScene();
    }

    private void Start()
    {
        // spawn an object at each spawn point at start
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            
            SpawnObject(i);
            
        }

    }

    private void Update()
    {
    // increment the spawn timer
    _spawnTimer += Time.deltaTime;

    // if the spawn timer has elapsed, spawn an object
    if (_spawnTimer >= _spawnInterval)
    {
        // reset the spawn timer
        _spawnTimer = 0f;

        // randomly select an object pool type
        TypeDeterminer.ColorEnum brickType = _characterTypesInScene[Random.Range(0, _characterTypesInScene.Count)];

        // get a game object of the selected pool type from the object pooler

        // if a game object was returned from the pooler, spawn it
        GameObject objToSpawn = objectPooler.GetEnemyFromPool(brickType);
        if (objToSpawn != null)
        {
            // randomly select a spawn point
            int spawnIndex = Random.Range(0, spawnPoints.Length);
            Transform spawnPoint = spawnPoints[spawnIndex];

            // check if there is any object already in the spawn point
            Collider[] colliders = Physics.OverlapSphere(spawnPoint.position, 0.5f);
            bool canSpawn = true;
            foreach (Collider col in colliders)
            {
                if (col.gameObject.CompareTag("Brick"))
                {
                    canSpawn = false;
                    break;
                }
            }

            // if there is no object in the spawn point, spawn the new object
            if (canSpawn)
            {
                // set the spawn object's position to the spawn point's position
                SpawnObject(spawnIndex,brickType);

                
            }
            else
            {
                // return the object to the pool since the spawn point is occupied
                objectPooler.ReturnEnemyPool(objToSpawn);
            }
        }
    }
    
}


    // spawns an object at the specified spawn point with the specified object pool type
    private void SpawnObject(int spawnPointIndex, TypeDeterminer.ColorEnum brickType)
    {
        
            TypeDeterminer.ColorEnum brickType = _characterTypesInScene[Random.Range(0, _characterTypesInScene.Count)];
        
        
        Transform spawnPoint = spawnPoints[spawnPointIndex];

        // get a game object of the selected pool type from the object pooler
        GameObject obj = objectPooler.GetEnemyFromPool(brickType);

        // set the spawn object's position to the spawn point's position
        obj.transform.position = spawnPoint.position;

        // activate the spawn object
        obj.SetActive(true);
        if (brickType!=0)
        {
            if(GameManager.AITargets[brickType] == null)
            {
                GameManager.AITargets[brickType] = new List<Transform>();
            }
            GameManager.AITargets[brickType].Add(spawnPoint);
            //Debug.Log("I have added " +brickType +" type of brick");
        }

    }
    
    private void FindCharactersInScene()
    {
        foreach (GameObject character in GameObject.FindGameObjectsWithTag("Character")) 
        {
            _typeDeterminer = character.GetComponent<TypeDeterminer>();
            if (_typeDeterminer != null) 
            {
                    
                _characterTypesInScene.Add(_typeDeterminer.ColorType);
                    
            }
        }
    }

    
    

}*/