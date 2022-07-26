using System;
using RuntimeAnchors;
using UnityEditor;
using UnityEngine;
using System.Linq;

namespace Characters.Alpha1
{
    /// <summary>
    /// <para>This component consumes input from the InputReader and stores its values. The input is then read, and manipulated, by the StateMachine's Actions.</para>
    /// </summary>
    public class Protagonist : MonoBehaviour
    {
        [SerializeField]
        private InputReader _inputReader;

        [SerializeField]
        private TransformAnchor _gameplayCameraTransform;
        
        private Vector2 _inputVector;
        private float _previousSpeed;

        //These fields are read and manipulated by StateMachine Actions;
        [NonSerialized]
        public Vector3 movementInput;
        [NonSerialized]
        public Vector3 movementVector;

        public const float GRAVITY_MULTIPLIER = 5f;
        public const float MAX_FALL_SPEED = -50f;
        public const float MAX_RISE_SPEED = 100f;
        public const float GRAVITY_COMEBACK_MULTIPLIER = .03f;
        public const float GRAVITY_DIVIDER = .6f;
        public const float AIR_RESISTANCE = 5f;

        //Adds listeners for events being triggered in the InputReader script
        private void OnEnable()
        {
            _inputReader.MoveEvent += OnMove;
            //...
        }

        //Removes all listeners to the events coming from the InputReader script
        private void OnDisable()
        {
            _inputReader.MoveEvent -= OnMove;
            //...
        }

        private void Update()
        {
            RecalculateMovement();
        }

        private void RecalculateMovement()
        {
            // Accelerate/Decelerate
            float targetSpeed = Mathf.Clamp01(_inputVector.magnitude);
            targetSpeed = Mathf.Lerp(_previousSpeed, targetSpeed, Time.deltaTime * 4.0f);
            
            // Assignment of Values
            movementInput = new Vector3(_inputVector.x, 0f, _inputVector.y);

            _previousSpeed = targetSpeed;
        }
        
        //---- Event Listeners ---- 

        private void OnMove(Vector2 movement)
        {
            _inputVector = movement;
        }
    }
}
