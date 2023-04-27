using Rimaethon._Scripts.Animation;
using Rimaethon._Scripts.Core;
using Rimaethon.Movement;
using UnityEngine;

namespace Rimaethon._Scripts.Movement
{
    public class PlayerMovement : CharacterRotator, IMoveAble
    {
        private CharacterController _characterController;
        [SerializeField] private float characterSpeed = 6f;
        private IAnimateAble _playerAnimation;

        

        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
            
            _playerAnimation = GetComponent<IAnimateAble>();
            CustomAwake();
        }

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        protected void Update()
        {
            HandleTouchInput();
           
            Move();
        }

        private void FixedUpdate()
        {
            HandleRotationWithTouchInput();
        }

        public void Move()
        {
            IsPlayerDragging = TouchDelta.sqrMagnitude < 0.1f;

            if (!IsPlayerTouchingScreen || IsPlayerDragging)
            {
                _playerAnimation.HandleAnimationRequest(CharacterAnimation.IsRunning, false);
                return;
            }

            // Calculate the custom direction vector
            Vector3 moveDirection = (transform.forward - transform.up).normalized;

            // Move the character in the custom direction
            _characterController.Move(moveDirection * (characterSpeed * Time.deltaTime));

            _playerAnimation.HandleAnimationRequest(CharacterAnimation.IsRunning, true);
        }

    }

}
