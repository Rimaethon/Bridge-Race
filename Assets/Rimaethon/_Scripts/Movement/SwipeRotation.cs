using UnityEngine;
using UnityEngine.InputSystem;

public class SwipeRotation : MonoBehaviour
{
    private float _swipeThreshold = 50f;
    private Vector2 _firstTouchPosition;
    private Vector2 _lastTouchPosition;

    [SerializeField] private float _rotationSpeed = 10f;

    private void OnEnable()
    {
        var playerTouch = new PlayerInputMaps();
        playerTouch.PlayerTouch.Move.performed += OnMove;
        playerTouch.PlayerTouch.FirstTouch.performed += OnFirstTouch;
        playerTouch.PlayerTouch.FirstTouch.canceled += OnTouchCanceled;
        playerTouch.Enable();
    }

    private void OnDisable()
    {
        var playerTouch = new PlayerInputMaps();
        playerTouch.PlayerTouch.Move.performed -= OnMove;
        playerTouch.PlayerTouch.FirstTouch.performed -= OnFirstTouch;
        playerTouch.PlayerTouch.FirstTouch.canceled -= OnTouchCanceled;
        playerTouch.Disable();
    }

    private void OnFirstTouch(InputAction.CallbackContext context)
    {
        _firstTouchPosition = context.ReadValue<Vector2>();
    }

    private void OnTouchCanceled(InputAction.CallbackContext context)
    {
        _firstTouchPosition = Vector2.zero;
        _lastTouchPosition = Vector2.zero;
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        if (_firstTouchPosition == Vector2.zero)
        {
            return;
        }

        _lastTouchPosition = context.ReadValue<Vector2>();

        if (Vector2.Distance(_lastTouchPosition, _firstTouchPosition) > _swipeThreshold)
        {
            Vector2 swipeDirection = _lastTouchPosition - _firstTouchPosition;
            float angle = Mathf.Atan2(swipeDirection.y, swipeDirection.x) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.LookRotation(new Vector3(swipeDirection.x, 0, swipeDirection.y), Vector3.up);
            float shortestAngle = Quaternion.Angle(transform.rotation, targetRotation) * Mathf.Sign(angle);
            Quaternion rotation = Quaternion.AngleAxis(shortestAngle, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * _rotationSpeed);
        }

    }
}