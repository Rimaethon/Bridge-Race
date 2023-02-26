using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerMovement : MonoBehaviour
{
    #region Fields

    
    [SerializeField] private Camera playerCamera;
    
    private PlayerInputMaps _playerInput;

    private InputAction _touchPositionAction;

    private InputAction _firstTouchAction;

    private CharacterController _characterController;
    private Animator _animator;
    


    public float rotationSpeed = 1f;
    private float _speed = 5f;
    private float _yRotation;
    private Vector2 _move;
    private Vector3 _moveDirection;
    private float _rotateAngle;
    private float _rotateSpeed=0.5f;
    private RaycastHit _hitInfo;
    private float _raycastValue;
    private float _targetVelocity=0.1f;
    private float _velocityThatWillBeAssigned;
    private InputControl _control;
    private bool _isRunning;
    private static readonly int IsRunning = Animator.StringToHash("IsRunning");
    private Vector2 _firstTouchPosition;
    private Vector2 _firstTouchNormalizedPosition;
    private Vector2 _touchDraggingPosition;
    private Vector2 _touchDraggingNormalizedPosition;

    #endregion

    
    #region OnEnableDisable
    private void OnEnable()
    {
        
        _playerInput.PlayerTouch.Enable();



    }
    
    private void OnDisable()
    {
        
        
        _playerInput.PlayerTouch.Disable();
    }
    

    #endregion
    
    
    #region UnityMethods


    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponentInChildren<Animator>();
        _playerInput = new PlayerInputMaps();
        _touchPositionAction = _playerInput.PlayerTouch.Move;
        _firstTouchAction = _playerInput.PlayerTouch.FirstTouch;
        
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        //_prevPos = transform.position;

    }
    
    void Update()
    {
        Move();
        Animate();
        //GiveFirstTouchPosition();

        // Debug.Log(_raycastValue);
    }

    
    

    #endregion

    
    
    #region CreatedMethods
    

    private void Move()
    {
        // Get the touch position in pixel coordinates
        _firstTouchPosition = _firstTouchAction.ReadValue<Vector2>();
    
        // Convert pixel coordinates to normalized device coordinates
        _firstTouchNormalizedPosition = new Vector2(
            (_firstTouchPosition.x / Screen.width) * 2 - 1,
            (_firstTouchPosition.y / Screen.height) * 2 - 1
        );
        
        
        _touchDraggingPosition = _touchPositionAction.ReadValue<Vector2>();
    
        // Convert pixel coordinates to normalized device coordinates
        _touchDraggingNormalizedPosition = new Vector2(
            (_touchDraggingPosition.x / Screen.width) * 2 - 1,
            (_touchDraggingPosition.y / Screen.height) * 2 - 1
        );
        
        Debug.Log("Im first touch" + _firstTouchNormalizedPosition);
        Debug.Log("Im dragging touch" + _touchDraggingNormalizedPosition);
        Debug.Log("Im their difference" +Vector2.Distance(_firstTouchNormalizedPosition,_touchDraggingNormalizedPosition));

        if (Physics.Raycast(transform.position, Vector3.down, out _hitInfo, _characterController.height / 2 + 0.1f))
        {
                _raycastValue = -_hitInfo.normal.y;
        }

        
    }
    


    private void Animate()
    {
        if (_move!= Vector2.zero)
        {
            _isRunning = true;
            _animator.SetBool(IsRunning,_isRunning);


        }
        else if(_move== Vector2.zero)
        {
            _isRunning = false;
            _animator.SetBool(IsRunning,_isRunning);
        }
    }

   


    #endregion

    
}