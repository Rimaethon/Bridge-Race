using UnityEngine;
using UnityEngine.InputSystem;

namespace Movement
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
                TouchDelta.Normalize();
            }
            else
            {
                _normalizedTouchDraggingPosition = _normalizedFirstTouchPosition;
            }


        }
        
        private Vector2 NormalizeTouchPosition(Vector2 touchPosition)
        {
            _normalizedX = (touchPosition.x / Screen.width) * 2 - 1;
            _normalizedY = (touchPosition.y / Screen.height) * 2 - 1;
            return new Vector2(_normalizedX, _normalizedY);
        } 
        
    }
}
