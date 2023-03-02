using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PoolSpawner : MonoBehaviour
{
    public int[] objectPoolTypes = new int[3]; // the object pool types to use (0, 1, or 2)
    public Transform[] spawnPoints; // the positions where objects will be spawned
    public float spawnInterval = 5f; // the time between spawns

    private float spawnTimer = 0f;
    public ObjectPooler objectPooler;
    private Transform player;

    // list of active spawned objects
    private List<GameObject> activeObjects = new List<GameObject>();
    List<int> inactiveSpawnPointIndices = new List<int>();

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
    spawnTimer += Time.deltaTime;

    // if the spawn timer has elapsed, spawn an object
    if (spawnTimer >= spawnInterval)
    {
        // reset the spawn timer
        spawnTimer = 0f;

        // randomly select an object pool type
        int poolTypeIndex = Random.Range(0, objectPoolTypes.Length);

        // get a game object of the selected pool type from the object pooler

        // if a game object was returned from the pooler, spawn it
        GameObject objToSpawn = objectPooler.GetEnemyFromPool(poolTypeIndex);
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
                objToSpawn.transform.position = spawnPoint.position;

                // activate the spawn object
                objToSpawn.SetActive(true);

                // add the object to the list of active objects
                activeObjects.Add(objToSpawn);
            }
            else
            {
                // return the object to the pool since the spawn point is occupied
                objectPooler.ReturnEnemyPool(objToSpawn);
            }
        }
    }

    // check for inactive objects and respawn them
    for (int i = activeObjects.Count - 1; i >= 0; i--)
    {
        GameObject obj = activeObjects[i];
        if (obj == null || !obj.activeSelf)
        {
            // remove the object from the list of active objects
            activeObjects.RemoveAt(i);

            // reset and respawn the object
            int spawnPointIndex = GetSpawnPointIndex(obj.transform);
            if (spawnPointIndex != -1)
            {
                SpawnObject(spawnPointIndex, Random.Range(0, objectPoolTypes.Length));
            }
        }
    }
}


    // spawns an object at the specified spawn point with the specified object pool type
    private void SpawnObject(int spawnPointIndex, int poolTypeIndex = -1)
    {
        if (poolTypeIndex == -1)
        {
            // randomly select an object pool type
            poolTypeIndex = Random.Range(0, objectPoolTypes.Length);
        }

        Transform spawnPoint = spawnPoints[spawnPointIndex];

        // get a game object of the selected pool type from the object pooler
        GameObject obj = objectPooler.GetEnemyFromPool(poolTypeIndex);

        // set the spawn object's position to the spawn point's position
        obj.transform.position = spawnPoint.position;

        // activate the spawn object
        obj.SetActive(true);

        // add the object to the list of active objects
        activeObjects.Add(obj);
    }

    // gets the index of the spawn point that the specified transform is at
    private int GetSpawnPointIndex(Transform objTransform)
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            if (spawnPoints[i] == objTransform)
            {
                return i;
            }
        }

        return -1;
    }

    // gets the index of a random inactive spawn point
    private int GetRandomInactiveSpawnPointIndex()
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            Transform spawnPoint = spawnPoints[i];

            // check if the spawn point has an inactive object
            bool hasInactiveObject = false;
            for (int j = 0; j < activeObjects.Count; j++)
            {
                GameObject obj = activeObjects[j];
                if (obj.transform.position == spawnPoint.position && !obj.activeSelf)
                {
                    hasInactiveObject = true;
                    break;
                }
            }


            // if the spawn point doesn't have an active object, add its index to the list of inactive spawn point indices
            if (!hasInactiveObject)
            {
                inactiveSpawnPointIndices.Add(i);
            }
        }

        // if there are no inactive spawn points, return -1
        if (inactiveSpawnPointIndices.Count == 0)
        {
            return -1;
        }

        // randomly select an index from the list of inactive spawn point indices and return it
        int randomIndex = Random.Range(0, inactiveSpawnPointIndices.Count);
        return inactiveSpawnPointIndices[randomIndex];
    }

}