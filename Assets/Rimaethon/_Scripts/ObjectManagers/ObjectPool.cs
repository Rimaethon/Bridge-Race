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

        
        [SerializeField] private GameObject brickPrefab;
        private List<ColorEnum> _brickTypeList;
        private int poolSize = 40;
        private PlatformStates _platformState;

        private void OnEnable()
        {
            EventManager.Instance.AddHandler<GameObject>(GameStates.OnCharacterLevelChange,HandleCharacterLevelChange);
            EventManager.Instance.AddHandler(GameStates.OnGameStart,InstantiateObjects);
        }

        private void OnDisable()
        {
            EventManager.Instance.RemoveHandler<GameObject>(GameStates.OnCharacterLevelChange,HandleCharacterLevelChange);
            EventManager.Instance.RemoveHandler(GameStates.OnGameStart,InstantiateObjects);
        }

        private void Awake()
        {

            _platformState = PlatformStates.StartingPlatform;
        }
        

        private void InstantiateObjects()
        {
            
            _brickTypeList =SceneDataHolder.CharactersTypesOnLevels[PlatformStates.StartingPlatform];

            foreach (ColorEnum color in _brickTypeList)
            {
                for (int i = 0; i < poolSize; i++)
                {
                    GameObject brickObject = Instantiate(brickPrefab);
                    brickObject.GetComponent<MpbController>().ColorType = color;
                    ReturnBrickToPool(brickObject);
                }
            }
            
            Debug.Log("yes ı finished instantiation but ı dont broadcast fucker");
            GameManager.instance.UpdateGameState(GameStates.OnObjectsInstantiated);
            
        }

        

        public GameObject GetBrickFromPool(ColorEnum colorType)
        {
            if (SceneDataHolder.PooledBrickDictionary[PooledObjectStatus.NotActive][colorType].Count == 0)
            {
                
                return null;
            }

            GameObject pooledBrick = SceneDataHolder.PooledBrickDictionary[PooledObjectStatus.NotActive][colorType][0];
            HandleBrickDictionary(PooledObjectStatus.Active,pooledBrick);
            //Debug.Log("I made"+colorType+" brick "+pooledBrick.transform.position+" in this position");
            return pooledBrick;
        }

        public void ReturnBrickToPool(GameObject brick)
        {
            if (brick.GetComponent<BoxCollider>().enabled == false)
            {
                brick.GetComponent<BoxCollider>().enabled = true;
            }   
            brick.transform.rotation = Quaternion.identity;
            brick.SetActive(false);
            HandleBrickDictionary(PooledObjectStatus.NotActive,brick);
        }
        
        

     


       public void HandleBrickDictionary(PooledObjectStatus NewStatus, GameObject brick)
        {
            ColorEnum color = brick.GetComponent<ITypeDeterminer>().ColorType;
            PooledObjectStatus oldStatus = brick.GetComponent<IPoolAble>().ObjectStatus;
            Debug.Log("I got an object with "+oldStatus+" and im gonna change it to"+NewStatus);
            
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



