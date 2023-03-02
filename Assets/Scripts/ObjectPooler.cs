using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.Serialization;

public class ObjectPooler : MonoBehaviour
{

    public GameObject[] brickPrefabs;
    [SerializeField] private int poolSize = 20;
    private List<GameObject>[] _brickPools;
    private GameObject _brick;



    
    void Awake()
    {
        _brickPools = new List<GameObject>[brickPrefabs.Length];
        for (int i = 0; i < brickPrefabs.Length; i++)
        {
            _brickPools[i] = new List<GameObject>();
            for (int j = 0; j < poolSize; j++)
            {
                _brick = Instantiate(brickPrefabs[i]);
                _brick.SetActive(false);
                _brickPools[i].Add(_brick);
            }
            
        }

    }
    
    

    public GameObject GetEnemyFromPool(int brickType)
    {
        for (int i = 0; i < _brickPools[brickType].Count; i++)
        {
            if (!_brickPools[brickType][i].activeInHierarchy)
            {
                
                return _brickPools[brickType][i];
            }
        }
        
        if (_brickPools[brickType].Count < poolSize)
        {
            _brick = Instantiate(brickPrefabs[brickType]);
            _brick.SetActive(false);
            _brickPools[brickType].Add(_brick);
            return _brick;
        }
    
        return null;
    }

    public void ReturnEnemyPool(GameObject brick)
    {
        brick.SetActive(false);
    }
}
