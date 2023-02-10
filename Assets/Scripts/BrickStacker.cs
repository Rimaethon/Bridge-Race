using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BrickStacker : MonoBehaviour
{
    #region  Fields
    [Header("Necessary Fields For Stack System")]
    [Tooltip("Player GameObject Reference For Collision detection")]
    [SerializeField] private GameObject player;
    [Tooltip("Brick Holder GameObject Reference For Determining Player's Holding Point Of Bricks")]
    [SerializeField] private GameObject brickHolder;
    [Tooltip("")]
    private PlayerMovement _playerMovementScript;
    private Vector3 _brickHolderPosition;
    private Transform _brickTransform;
    private float _brickAscend=0.1f;
    private int _brickId;


    #endregion
    

    #region Start And Uptade Methods
    void Start()
    {
        _brickTransform = gameObject.transform;
        _playerMovementScript=player.GetComponent<PlayerMovement>();
        
    }

    // Update is called once per frame
    void Update()
    {
        

    }
    

    #endregion


    #region  Private Methods
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player && _playerMovementScript.playerId==_brickId)
        {
            _brickHolderPosition = brickHolder.transform.position;
            _brickHolderPosition.y += _brickAscend;
            _brickTransform.position = _brickHolderPosition;
            _brickTransform.parent = brickHolder.transform;
            gameObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
            _brickAscend += 0.5f;





        }
    }
    

    #endregion
   
}
