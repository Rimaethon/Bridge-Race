using _Scripts.Core;
using Rimaethon._Scripts.Animation;
using Rimaethon.Movement;
using UnityEngine;

namespace Rimaethon._Scripts.Movement
{
    public class PlayerMovement : CharacterRotator, IMoveAble
    {
        private CharacterController _characterController;
        [SerializeField] private float characterSpeed = 6f;
        private PlayerAnimation _playerAnimation;

        

        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
            
            _playerAnimation = GetComponent<PlayerAnimation>();
            CustomAwake();
        }

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        protected void Update()
        {
            HandleTouchInput();
            HandleRotationWithTouchInput();
            Move();
        }

        private void FixedUpdate()
        {
            _characterController.Move(transform.up * -0.1f);
        }

        public void Move()
        {
            IsPlayerDragging = TouchDelta.sqrMagnitude < 0.1f;

            if (!IsPlayerTouchingScreen || IsPlayerDragging)
            {
                _playerAnimation.HandleAnimationRequest(CharacterAnimation.IsRunning, false);
                return;
            }
        
            _characterController.Move(transform.forward * (characterSpeed * Time.deltaTime));
        
            _playerAnimation.HandleAnimationRequest(CharacterAnimation.IsRunning, true);
        }
    }

}
