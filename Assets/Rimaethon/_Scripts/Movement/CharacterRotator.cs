using System;
using Rimaethon._Scripts.Movement;
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
        [SerializeField] private float rotationSpeed=10f;

        private void Start()
        {
            

        }

        protected void HandleRotationWithTouchInput()
        {
            if (TouchDelta.sqrMagnitude < 0.0005f)
            {
                return;
            }
    
            targetCharacterAngle = Mathf.Atan2(TouchDelta.x, TouchDelta.y) * Mathf.Rad2Deg;
    
            Debug.Log("target angle is "+targetCharacterAngle+" and current angle is"+currentCharacterAngle);

            float angleDifference = Mathf.DeltaAngle(currentCharacterAngle, targetCharacterAngle);
           

            if (Mathf.Abs(angleDifference) > rotationSpeed)
            {
                newCharacterAngle = currentCharacterAngle + Mathf.Sign(angleDifference) *rotationSpeed;
            }
            else
            {
                newCharacterAngle = targetCharacterAngle;
            }

            currentCharacterAngle = newCharacterAngle;
            transform.eulerAngles = new Vector3(0f, newCharacterAngle, 0f);
        }



    }
}