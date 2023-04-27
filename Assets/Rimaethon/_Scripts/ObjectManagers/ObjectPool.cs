using System;
using System.Collections.Generic;
using Rimaethon._Scripts.Core;
using Rimaethon._Scripts.Core.Enums;
using Rimaethon._Scripts.Core.Interfaces;
using Rimaethon._Scripts.Managers;
using UnityEngine;

namespace Rimaethon._Scripts.ObjectManagers
{
    public class ObjectPool : MonoBehaviour,IObjectPool
    {

        
        private int poolSize = 40;
        [SerializeField] private GameObject brickPrefab;
        private List<ColorEnum> _brickTypeList;
        private PlatformStates _platformState;

        private void OnEnable()
        {
            EventManager.Instance.AddHandler<GameObject>(GameStates.OnCharacterLevelChange,HandleCharacterLevelChange);
        }

        private void Awake()
        {

            InstantiateObjects(poolSize);
            _platformState = PlatformStates.StartingPlatform;
        }
        

        private void InstantiateObjects(int objectPoolSize)
        {
            
            _brickTypeList =SceneDataHolder.CharactersTypesOnLevels[PlatformStates.StartingPlatform];

            foreach (ColorEnum color in _brickTypeList)
            {
                for (int i = 0; i < objectPoolSize; i++)
                {
                    GameObject brickObject = Instantiate(brickPrefab);
                    brickObject.GetComponent<MpbController>().ColorType = color;
                    ReturnBrickToPool(brickObject);
                }
            }
            Debug.Log("yes ı finished instantiation but ı dont broadcast fucker");
            EventManager.Instance.Broadcast<PlatformStates>(GameStates.OnObjectsInstantiated,PlatformStates.StartingPlatform);
            
        }

        

        public GameObject GetBrickFromPool(ColorEnum colorType)
        {
            if (SceneDataHolder.PooledBrickDictionary[PooledObjectStatus.NotActive][colorType].Count == 0)
            {
                
                return null;
            }

            GameObject pooledBrick = SceneDataHolder.PooledBrickDictionary[PooledObjectStatus.NotActive][colorType][0];
            HandleBrickDictionary(PooledObjectStatus.Active,pooledBrick);
            Debug.Log("I made"+colorType+" brick "+pooledBrick.transform.position+" in this position");
            return pooledBrick;
        }

        public void ReturnBrickToPool(GameObject brick)
        {
            if (brick.GetComponent<BoxCollider>().enabled == false)
            {
                brick.GetComponent<BoxCollider>().enabled = true;
            }   
            brick.SetActive(false);
            HandleBrickDictionary(PooledObjectStatus.NotActive,brick);
        }
        
        

     


       public void HandleBrickDictionary(PooledObjectStatus NewStatus, GameObject brick)
        {
            ColorEnum color = brick.GetComponent<ITypeDeterminer>().ColorType;
            PooledObjectStatus oldStatus = brick.GetComponent<IPoolAble>().ObjectStatus;
            
            if (!SceneDataHolder.PooledBrickDictionary[NewStatus].ContainsKey(color))
            {
                SceneDataHolder.PooledBrickDictionary[NewStatus].Add(color, new List<GameObject>());
            }
            
            if (oldStatus == NewStatus)
            {
                SceneDataHolder.PooledBrickDictionary[NewStatus][color].Add(brick);
            }
            else
            {
                SceneDataHolder.PooledBrickDictionary[NewStatus][color].Add(brick);
                SceneDataHolder.PooledBrickDictionary[oldStatus][color].Remove(brick);
                brick.GetComponent<MpbController>().ObjectStatus = NewStatus;
            }

        }

       void HandleCharacterLevelChange(GameObject character)
       {
           ColorEnum characterType = character.GetComponent<ITypeDeterminer>().ColorType;
           List<GameObject> activeBricks = SceneDataHolder.PooledBrickDictionary[PooledObjectStatus.Active][characterType];

           // Create a copy of the list to avoid modifying the original list while iterating over it
           List<GameObject> bricksToReturn = new List<GameObject>(activeBricks);

           foreach (GameObject brick in bricksToReturn)
           {
               ReturnBrickToPool(brick);
           }
       }

    }
}



