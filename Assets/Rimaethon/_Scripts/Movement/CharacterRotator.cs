using Movement;
using UnityEngine;

namespace Rimaethon.Movement
{
    public abstract class CharacterRotator : PlayerInputHandler
    {
        private Vector2 previousTouchPosition;
        private Vector2 currentTouchPosition;
        
        private float targetCharacterAngle;
        private float currentCharacterAngle;
        private float newCharacterAngle;
        [SerializeField] private float rotationSpeed=200f;


        protected void HandleRotationWithTouchInput()
        {
            if (TouchDelta.sqrMagnitude < 0.0001f)
            {
                return;
            }
            
            targetCharacterAngle = Mathf.Atan2(TouchDelta.x, TouchDelta.y) * Mathf.Rad2Deg;
            
            
            newCharacterAngle = Mathf.LerpAngle(currentCharacterAngle, targetCharacterAngle, rotationSpeed * Time.deltaTime);
            
            transform.eulerAngles = new Vector3(0f, newCharacterAngle, 0f);
        }
        
        


    }
}