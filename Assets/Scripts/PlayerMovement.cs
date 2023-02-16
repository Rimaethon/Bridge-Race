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

    private InputAction _touchPressAction;

    private CharacterController _characterController;
    private Animator _animator;
    
    private bool _isRunning;
    public int playerId;


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
    private Vector3 _prevPos;
    private static readonly int Velocity = Animator.StringToHash("Velocity");
    private InputControl _control;
    

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
        _touchPressAction = _playerInput.PlayerTouch.TouchPress;
        _touchPositionAction.performed += OnPlayerAction;

    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _prevPos = transform.position;

    }
    
    void Update()
    {
        _animator.GetFloat(Velocity);
        Move();

        // Debug.Log(_raycastValue);
    }

    private void FixedUpdate()
    {
        Animate();
        _velocityThatWillBeAssigned = Mathf.Lerp(_animator.GetFloat(Velocity), _targetVelocity - 0.1f, 0.1f);
        
        _animator.SetFloat(Velocity,_velocityThatWillBeAssigned);

    }
    

    #endregion

    
    
    #region CreatedMethods

    private void Look()
    {
        if (_prevPos != transform.position) 
        {
            _moveDirection = transform.position - _prevPos;
            _rotateAngle = Mathf.Atan2(_moveDirection.z, _moveDirection.x) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(0, -1*_rotateAngle, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * _rotateSpeed);
            _prevPos = transform.position;
        }

        
    }

    private void Move()
    {
        
        _move = _touchPositionAction.ReadValue<Vector2>();
        _move.Normalize();

        if (_move.magnitude < 0.1f)
        {
            return; 
        }

        if (Physics.Raycast(transform.position, Vector3.down, out _hitInfo, _characterController.height / 2 + 0.1f))
        {
            _raycastValue = -_hitInfo.normal.y;
        }

        Vector3 touchPosition = new Vector3(_move.x, _raycastValue, _move.y);

        _characterController.Move(touchPosition * (_speed * Time.deltaTime));

    }


    private void Animate()
    {
        if (_touchPositionAction.ReadValue<Vector2>() != Vector2.zero)
        {
            if ((_targetVelocity + (_targetVelocity / 10)) <= 1.1)
            {
                
                _targetVelocity+=(_targetVelocity/10);
            }
            else
            {
                _targetVelocity = 1.1f;
            }
            
            
        }
        else if( _touchPositionAction.ReadValue<Vector2>() == Vector2.zero)
        {
            if (_targetVelocity-(_targetVelocity/6)>=0.1)
            {
                _targetVelocity-=(_targetVelocity/6);
            }
            else
            {
                _targetVelocity = 0.1f;
            }
        }
    }

    public void OnPlayerAction(InputAction.CallbackContext context)
    {
        InputControl control = context.control;
        
        if (control.path == "<Touchscreen>/delta")
        {
          
            Vector2 moveInput = context.ReadValue<Vector2>();
            Debug.Log(moveInput);
         
        }
        else if (control.path == "<Touchscreen>/primaryTouch/startPosition")
        {
            
            Vector2 touchPosition = context.ReadValue<Vector2>(); 
            Debug.Log(touchPosition );
        }
    }


    #endregion

    
}