using UnityEngine;
using UnityEngine.InputSystem;

namespace Rimaethon
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private Camera playerCamera;
        private CharacterController characterController;
        [SerializeField] private float characterSpeed=6f;
        private bool isCharacterMoving;
        private Animator animator;
        private static readonly int IsRunning = Animator.StringToHash("IsRunning");

        private PlayerInputMaps playerInput;
        private InputAction touchMoveAction;
        private InputAction firstTouchAction;
        private InputAction touchHoldAction;

        private Vector2 previousTouchPosition;
        private Vector2 currentTouchPosition;
        private Vector2 deltaTouch;
        private Vector2 firstTouchNormalizedPosition;
        private Vector2 touchDraggingNormalizedPosition;
        private bool isPlayerHoldingFinger;
        private float targetCharacterAngle;
        private float currentCharacterAngle;
        private float newCharacterAngle;
        [SerializeField] private float rotationSpeed=150f;

        private void Awake()
        {
            characterController = GetComponent<CharacterController>();
            animator = GetComponentInChildren<Animator>();
            playerInput = new PlayerInputMaps();
            touchMoveAction = playerInput.PlayerTouch.Move;
            firstTouchAction = playerInput.PlayerTouch.FirstTouch;
            touchHoldAction = playerInput.PlayerTouch.TouchHolding;
        }

        private void OnEnable()
        {
            playerInput.PlayerTouch.Enable();
        }

        private void OnDisable()
        {
            playerInput.PlayerTouch.Disable();
        }

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void Update()
        {
            HandleTouchInput();
            HandleRotationWithTouchInput();
            Move();
            Animate();
        }

        private void FixedUpdate()
        {
            // Apply a constant downward force to the player character to simulate gravity.
            characterController.Move(transform.up * -0.1f);
        }

        private void Move()
        {
            // Check if the player is not touching the screen or the touch movement is too small to move the character.
            if (!isPlayerHoldingFinger || deltaTouch.sqrMagnitude < 0.1f)
            {
                isCharacterMoving = false;
                return;
            }

            // Move the character forward in the direction of the touch movement.
            characterController.Move(transform.forward * characterSpeed * Time.deltaTime);
            isCharacterMoving = true;
        }

        private void Animate()
        {
            animator.SetBool(IsRunning, isCharacterMoving);
        }

        private void HandleRotationWithTouchInput()
        {
            deltaTouch = touchDraggingNormalizedPosition - firstTouchNormalizedPosition;
            deltaTouch.Normalize();
            // Check if the player is not touching the screen or the touch movement is too small to rotate the character.
            // Check if the player is not touching the screen or the touch movement is too small to rotate the character.
            if (deltaTouch.sqrMagnitude < 0.0001f)
            {
                return;
            }


            // Calculate the target angle based on the touch movement direction.
            targetCharacterAngle = Mathf.Atan2(deltaTouch.x, deltaTouch.y) * Mathf.Rad2Deg;

            // Interpolate the current angle towards the target angle at a constant speed.
            newCharacterAngle = Mathf.LerpAngle(currentCharacterAngle, targetCharacterAngle, rotationSpeed * Time.deltaTime);

            // Set the rotation of the character based on the current angle.
            transform.eulerAngles = new Vector3(0f, newCharacterAngle, 0f);
        }




        protected virtual void HandleTouchInput()
        {
            if (firstTouchAction == null || touchMoveAction == null || touchHoldAction == null)
            {
                Debug.LogError("Input actions are not initialized.");
                return;
            }

            Vector2 firstTouchPosition = firstTouchAction.ReadValue<Vector2>();
            Vector2 touchMovePosition = touchMoveAction.ReadValue<Vector2>();
            float touchHoldValue = touchHoldAction.ReadValue<float>();

            // Calculate normalized touch positions
            Vector2 normalizedFirstTouchPosition = NormalizeTouchPosition(firstTouchPosition);
            Vector2 normalizedTouchMovePosition = NormalizeTouchPosition(touchMovePosition);

            // Set properties for handling touch input
            firstTouchNormalizedPosition = normalizedFirstTouchPosition;
            touchDraggingNormalizedPosition = normalizedTouchMovePosition;
            isPlayerHoldingFinger = touchHoldValue > float.Epsilon;
        }

// Helper method to normalize touch positions
        private Vector2 NormalizeTouchPosition(Vector2 touchPosition)
        {
            float normalizedX = (touchPosition.x / Screen.width) * 2 - 1;
            float normalizedY = (touchPosition.y / Screen.height) * 2 - 1;
            return new Vector2(normalizedX, normalizedY);
        }
    }
}
// Get the normalized position of the first touch on the screen
