using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.InputSystem;



public abstract class InputHandlerBase : MonoBehaviour
{
    private PlayerInputMaps _playerInput;
    protected InputAction TouchPositionAction;
    protected InputAction FirstTouchAction;
    protected InputAction TouchHoldAction;
    
    protected Vector2 FirstTouchNormalizedPosition;
    protected Vector2 TouchDraggingNormalizedPosition;
    protected Vector2 PreviousTouchPosition;
    protected Vector2 CurrentTouchPosition;

    


    private void OnEnable()
    {
        
        _playerInput.PlayerTouch.Enable();



    }
    
    private void OnDisable()
    {
        _playerInput.PlayerTouch.Disable();
        
    }

    private void Awake()
    {
        _playerInput = new PlayerInputMaps();
        TouchPositionAction = _playerInput.PlayerTouch.Move;
        FirstTouchAction = _playerInput.PlayerTouch.FirstTouch;
        TouchHoldAction = _playerInput.PlayerTouch.TouchHolding;
    }
    
    void HandleRotationWithTouchInput()
    {
        FirstTouchNormalizedPosition = new Vector2(
            (FirstTouchAction.ReadValue<Vector2>().x / Screen.width) * 2 - 1,
            (FirstTouchAction.ReadValue<Vector2>().y / Screen.height) * 2 - 1
        );
        
    
        
        TouchDraggingNormalizedPosition = new Vector2(
            (TouchPositionAction.ReadValue<Vector2>().x / Screen.width) * 2 - 1,
            (TouchPositionAction.ReadValue<Vector2>().y / Screen.height) * 2 - 1
        );
        
        
        _previousTouchPosition = FirstTouchNormalizedPosition;
        _currentTouchPosition = TouchDraggingNormalizedPosition;
        
        float difference = Vector2.Distance(_currentTouchPosition, _previousTouchPosition);
        
        if (difference > 0.01f )
        {
            _targetAngle = Mathf.Atan2(TouchDraggingNormalizedPosition.y-FirstTouchNormalizedPosition.y, TouchDraggingNormalizedPosition.x-FirstTouchNormalizedPosition.x) * -57.2957795f + 90f;
            _currentAngle = transform.eulerAngles.y;

        }
        
        float newAngle = Mathf.LerpAngle(_currentAngle, _targetAngle, smoothSpeed * Time.deltaTime);
        transform.eulerAngles = new Vector3(0f, newAngle, 0f);
            
        if (Math.Abs(newAngle - _targetAngle) < 2)
        {
            _previousTouchPosition = _currentTouchPosition;
        }

        _ısPlayerHoldingFinger = _touchHoldAction.ReadValue<float>() > float.Epsilon ;
        
        _ısRunning = (difference > 0.1f && _ısPlayerHoldingFinger);
        
    }

    void HandleMovementWithTouchInput()
    {
        
    }
}

