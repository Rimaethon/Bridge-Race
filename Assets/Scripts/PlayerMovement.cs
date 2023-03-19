using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerMovement : MonoBehaviour,IAnimateAble
{
    #region Fields

    
    [SerializeField] private Camera playerCamera;
    
    

    private CharacterController _characterController;
    
    
    
    #endregion

    
    #region OnEnableDisable
    
    

    #endregion
    
    
    #region UnityMethods


    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        
        
        
        

    }
    
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

    }
    
    void Update()
    {
        Move();
        Animate(); 
        
        
    }
    private void FixedUpdate()
    {
        if (_ısRunning)
        {
            _characterController.Move(transform.forward * _speed );
            
            
        }
        _characterController.Move(transform.up * -0.1f );
    }

    
    

    #endregion

    
    
    #region CreatedMethods
    

    private void Move()
    {   
        
        
        
        

    }


    private void HandleTouchRotationAndMovement()
    {
        
        
    }

    private void Animate()
    {
        switch (_ısRunning)
        {
            case true:
                _isRunning = true;
                _animator.SetBool(IsRunning,_isRunning);
                break;
            default:
                _isRunning = false;
                _animator.SetBool(IsRunning,_isRunning);
                break;
        }  
    }

   


    #endregion

    
}