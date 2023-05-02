using System;
using Rimaethon._Scripts.Core;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Rimaethon._Scripts.Movement
{
    public abstract class PlayerInputHandler : MonoBehaviour
    {
        private InputAction _touchDraggingAction;
        private InputAction _firstTouchAction;
        private InputAction _touchHoldAction;
        private PlayerInputMaps _playerInput;


        protected Vector2 TouchDelta;
        private Vector2 _normalizedFirstTouchPosition;
        private Vector2 _normalizedTouchDraggingPosition;
        protected bool IsPlayerTouchingScreen;
        protected bool IsPlayerDragging;
        private float _normalizedY;
        private float _normalizedX;

     
        private void OnDisable()
        {
            _playerInput.PlayerTouch.Disable();
        }


        protected void CustomAwake()
        {
            _playerInput = new PlayerInputMaps();
            _playerInput.PlayerTouch.Enable();
            _touchDraggingAction = _playerInput.PlayerTouch.Move;
            _firstTouchAction = _playerInput.PlayerTouch.FirstTouch;
            _touchHoldAction = _playerInput.PlayerTouch.TouchHolding;
        }
        private void Awake()
        {
            CustomAwake();

        }


        protected void HandleTouchInput()
        {
            IsPlayerTouchingScreen = _touchHoldAction.ReadValue<float>() > 0.1f;

            if (IsPlayerTouchingScreen)
            {
                _normalizedFirstTouchPosition = NormalizeTouchPosition(_firstTouchAction.ReadValue<Vector2>());
                _normalizedTouchDraggingPosition = NormalizeTouchPosition(_touchDraggingAction.ReadValue<Vector2>());
                TouchDelta = _normalizedTouchDraggingPosition - _normalizedFirstTouchPosition;
            }
            else
            {
                _normalizedTouchDraggingPosition = _normalizedFirstTouchPosition;
            }
        }

        private Vector2 NormalizeTouchPosition(Vector2 touchPosition)
        {
            Ray ray = Helpers.Camera.ScreenPointToRay(touchPosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Debug.DrawLine(ray.origin, hit.point, Color.green, 1f);
                return hit.textureCoord;
            }

            Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red, 1f);
            return touchPosition;
        }

    }
}
