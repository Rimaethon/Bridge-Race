using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.Serialization;

public class ObjectPooler : MonoBehaviour
{
    [SerializeField] private int poolSize = 20;
    [SerializeField] private GameObject brickPrefab;
    private GameObject _brick;
    private GameObject[] characterObjects;
    
    

    private void Awake()
    {
        
        
    }


    void Start()
    {
        List<TypeDeterminer.ColorEnum> 
        foreach (TypeDeterminer.ColorEnum ColorType in GameDataHolder.CharactersInScene.Values)
        {
            if (!GameDataHolder.BrickPools.ContainsKey(ColorType))
            {
                GameDataHolder.BrickPools[ColorType] = new List<GameObject>();
            }
            for (int j = 0; j < poolSize; j++)
            {
                _brick = Instantiate(brickPrefab,gameObject.transform);
                _brick.SetActive(false);
                GameDataHolder.BrickPools[ColorType].Add(_brick);
            }
        }
            
            
        

    }
    
    

    /*public GameObject GetEnemyFromPool(int brickType)
    {
        if (_brickPools[brickType] == null)
        {
            Debug.LogError("Brick pool is null for type " + brickType);
            return null;
        }

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
    }*/
    
    
    
}
