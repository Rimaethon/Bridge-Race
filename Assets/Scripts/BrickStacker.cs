/*
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UIElements;

public class BrickStacker : MonoBehaviour
{
    #region  Fields

    private TypeDeterminer.ColorEnum _newBricksColor;
    private TypeDeterminer.ColorEnum _charactersColor;
    private int _brickCount;
    private GameObject _brickHolder;
    private Vector3 _brickHolderPosition;
    private GameObject _newBrick;
    private GameObject _collidedStair;
    
    
    #endregion
    
    

    #region UnityMethods

    private void Start()
    {
        _charactersColor = gameObject.GetComponent<TypeDeterminer>().ColorType;
        _brickHolder = gameObject.transform.GetChild(1).gameObject;
        
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.CompareTag("Brick"))
        {
            CollectBrick(other);
            
            
        }else if(other.gameObject.CompareTag("Stair"))
        {

            PutBrickToStair(other);
        }
    }

    

    #endregion


    #region  Custom Methods

    private void CollectBrick(Collider other)
    {
        
        _newBrick = other.gameObject;
        _newBricksColor = _newBrick.GetComponent<TypeDeterminer>().ColorType;
        
            
        if (_charactersColor==_newBricksColor)
        {    
            
            if (!GameDataHolder.bricksOnCharacters.ContainsKey(_charactersColor))
            {
                GameDataHolder.bricksOnCharacters[_charactersColor] = new List<GameObject>();
            }
            _newBrick.transform.position = _brickHolder.transform.position+ new Vector3(0, 0.105f *GameDataHolder.bricksOnCharacters[_charactersColor].Count , 0);
            _newBrick.transform.parent = _brickHolder.transform;
            _newBrick.transform.localRotation = Quaternion.Euler(0, 0, 0);
            _newBrick.GetComponent<BoxCollider>().enabled = false;
            GameDataHolder.bricksOnCharacters[_charactersColor].Add(_newBrick);

            EventManager.Broadcast(GameEvent.OnCollectingBrick,_charactersColor);
        }
        
    }
    
    
    private void PutBrickToStair(Collider other)
    {
        _collidedStair = other.gameObject;
        _collidedStairsColor = _collidedStair.GetComponent<TypeDeterminer>().ColorType;
        Debug.Log(_collidedStairsColor);

        if (_charactersColor!=_collidedStairsColor)
        {
            Debug.Log("ım colliding");
            EventManager.Broadcast(GameEvent.OnPuttingBrick,_collidedStair,_charactersColor);
            gameObject.GetComponent<CharacterController>().stepOffset= GameDataHolder.characterStepOffsets[_charactersColor];
        }
        else
        {
            gameObject.GetComponent<CharacterController>().stepOffset = 0.35f;
        }

        
    }
    #endregion

}
*/


