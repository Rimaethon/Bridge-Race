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
    
    private PlayerInputs _playerInput;
    private InputAction _moveInputAction;
    private CharacterController _characterController;
    private InputAction _lookInputAction;
    private Animator _animator;
    
    private bool _isRunning;
    public int playerId;
    
    
    
    public float rotationSpeed = 1f;
    private float _speed = 5f;
    private Vector2 _look;
    private float _yRotation;
    private Vector2 _move;
    private Vector3 _moveDirection;
    private RaycastHit hitInfo;
    private float _velocity=0f;
    private Vector2 vector;
    private static readonly int Velocity = Animator.StringToHash("Velocity");

    #endregion
    

    private void OnEnable()
    {
        
        _playerInput.Player.Enable();
        
        
    }
    
    private void OnDisable()
    {
        _moveInputAction.Disable();
        _lookInputAction.Disable();
        
    }
    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponentInChildren<Animator>();
        _playerInput = new PlayerInputs();
        _moveInputAction = _playerInput.Player.Movement;
        _lookInputAction = _playerInput.Player.Look;







    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        
    }
    
    void Update()
    {
        
        Move();
        Look();
        

    }

    private void FixedUpdate()
    {
        Animate();
        Debug.Log(_velocity);
    }


    private void Look()
    {
        
        _yRotation= playerCamera.transform.eulerAngles.y;
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, _yRotation, transform.eulerAngles.z); 
    }

    private void Move() 
    {
        _move = _moveInputAction.ReadValue<Vector2>();
        Debug.Log(_move);
        _moveDirection = new Vector3(_move.x, 0, _move.y);

        _moveDirection = transform.TransformDirection(_moveDirection);


        
        if (Physics.Raycast(transform.position, Vector3.down, out hitInfo, _characterController.height / 2 + 0.1f))
        {
            _moveDirection.y = -hitInfo.normal.y;
        }
        _characterController.Move(_moveDirection * (_speed * Time.deltaTime));
        
        
        
    }

    private void Animate()
    {
        _animator.SetFloat(Velocity, _velocity);
        if (_moveInputAction.ReadValue<Vector2>() != Vector2.zero && _velocity<1)
        {
            _velocity+=0.1f;
            Debug.Log("Im working bitch");
        }
        else if(_velocity>0)
        {
            _velocity-=0.1f;
        }
    }
    
   
    
    
}