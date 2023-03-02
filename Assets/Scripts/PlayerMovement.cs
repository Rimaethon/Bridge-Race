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
    private InputAction _touchHoldAction;

    private CharacterController _characterController;
    private Animator _animator;
    


    
    private float _speed = 4f;
    private float _yRotation;
    private Vector2 _move;
    private Vector3 _moveDirection;
    private float _rotateAngle;
    private RaycastHit _hitInfo;
    private float _raycastValue;
    private float _velocityThatWillBeAssigned;
    private InputControl _control;
    private bool _isRunning;
    private static readonly int IsRunning = Animator.StringToHash("IsRunning");
    private Vector2 _firstTouchPosition;
    private Vector2 _firstTouchNormalizedPosition;
    private Vector2 _touchDraggingPosition;
    private Vector2 _touchDraggingNormalizedPosition;
    public float smoothSpeed = 20f;
    private Vector2 _previousTouchPosition;
    private Vector2 _currentTouchPosition;
    private float _targetAngle;
    private float _currentAngle;
    private bool _ısPlayerWantsToMoveCharacter;

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
        _touchHoldAction = _playerInput.PlayerTouch.TouchHolding;

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

    
    

    #endregion

    
    
    #region CreatedMethods
    

    private void Move()
    {
        
        _firstTouchPosition = _firstTouchAction.ReadValue<Vector2>();

        
        _firstTouchNormalizedPosition = new Vector2(
            (_firstTouchPosition.x / Screen.width) * 2 - 1,
            (_firstTouchPosition.y / Screen.height) * 2 - 1
        );
        
        
        _touchDraggingPosition = _touchPositionAction.ReadValue<Vector2>();
    
        
        _touchDraggingNormalizedPosition = new Vector2(
            (_touchDraggingPosition.x / Screen.width) * 2 - 1,
            (_touchDraggingPosition.y / Screen.height) * 2 - 1
        );
        
        //Debug.Log("Im first touch" + _firstTouchNormalizedPosition);
        // Debug.Log("Im dragging touch" + _touchDraggingNormalizedPosition);
        //Debug.Log("Im their difference" + (_touchDraggingNormalizedPosition - _firstTouchNormalizedPosition));

        _previousTouchPosition = _firstTouchNormalizedPosition;

        _currentTouchPosition = _touchDraggingNormalizedPosition;
        float difference = Vector2.Distance(_currentTouchPosition, _previousTouchPosition);
        
        if (difference > 0.01f )
        {
            
            _targetAngle = Mathf.Atan2(_touchDraggingNormalizedPosition.y-_firstTouchNormalizedPosition.y, _touchDraggingNormalizedPosition.x-_firstTouchNormalizedPosition.x) * -57.2957795f + 90f;
            _currentAngle = transform.eulerAngles.y;
            
            

            // Apply the new angle to the transform
            
            
        }
        
        float newAngle = Mathf.LerpAngle(_currentAngle, _targetAngle, smoothSpeed * Time.deltaTime);
        transform.eulerAngles = new Vector3(0f, newAngle, 0f);
            
        if (Math.Abs(newAngle - _targetAngle) < 2)
        {
            _previousTouchPosition = _currentTouchPosition;
        }
        // ReSharper disable once CompareOfFloatsByEqualityOperator
        if (difference > 0.1f && _touchHoldAction.ReadValue<float>()==1)

        {
            _ısPlayerWantsToMoveCharacter = true;

        }else if (_touchHoldAction.ReadValue<float>() == 0)
        {
            _ısPlayerWantsToMoveCharacter = false;
        }
       
        
        
        
            
        
        
    }

    private void FixedUpdate()
    {
        if (_ısPlayerWantsToMoveCharacter)
        {
            _characterController.SimpleMove(transform.forward * _speed  );
            
        }
    }

    private void Animate()
    {
        switch (_ısPlayerWantsToMoveCharacter)
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