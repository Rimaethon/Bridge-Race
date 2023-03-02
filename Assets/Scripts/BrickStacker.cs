using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class BrickStacker : MonoBehaviour
{
    #region  Fields
    [Header("Necessary Fields For Stack System")]
    [Tooltip("Brick Holder GameObject Reference For Determining Player's Holding Point Of Bricks")]
    [SerializeField] private GameObject brickHolder;
    [Tooltip("")]
    private PlayerMovement _playerMovementScript;
    private Vector3 _brickHolderPosition;
    private float _brickAscend=0.1f;
    public List<GameObject> bricksOnPlayer;
    public delegate void  BrickCollectingAction();
    public static BrickCollectingAction brickCollectingAction;
    private ObjectPooler _objectPooler;
    private Character _character;
    public int _brickCount=0;
    #endregion
    

    #region UnityMethods
    void Start()
    {
        _character = gameObject.GetComponent<Character>();
        
        
        
        
    }

    // Update is called once per frame
    void Update()
    {
        

    }
    

    #endregion


    #region  Private Methods

    private void OnTriggerEnter(Collider hit)
    {

        if (hit.gameObject.CompareTag("Brick"))
        {
            if (CharacterManager.Characters[_character]==hit.gameObject.GetComponent<Brick>().brickType )
            {
                Debug.Log("Confirmed");

                GameObject _brickHolder = gameObject.transform.GetChild(1).gameObject;
                Debug.Log(_brickHolder.tag);
                _brickHolderPosition = _brickHolder.transform.position ;
                hit.gameObject.transform.position = _brickHolderPosition+new Vector3(0,_brickAscend*_brickCount,0);
            
                hit.gameObject.transform.parent = _brickHolder.transform;
                hit.gameObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
                hit.gameObject.GetComponent<BoxCollider>().enabled = false;
                bricksOnPlayer.Add(hit.gameObject);
                _brickCount++;
                //If ı start the array from 0 as usual the brickputter script's oncontrollercolliderhit method becomes able to put bricks when it has 0 brick

            }
            
            
        }
        
        
    }
    

    #endregion
   
}
