using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private Vector2 _movementInput;
    private Vector2 _currentMovementInput;
    private Vector2 _currentVelocity;
    
    [SerializeField]
    private float _speed;
    private float _smoothTime = 0.1f;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        // Smooth the Player movement
        // SmoothDamp(float current, float target, ref float currentVelocity, float smoothTime, float maxSpeed = Mathf.Infinity, float deltaTime = Time.deltaTime)
        _currentMovementInput = Vector2.SmoothDamp(
            _currentMovementInput,
            _movementInput,
            ref _currentVelocity, 
            _smoothTime);

        // move player gameobject
        _rigidbody.velocity = _currentMovementInput * _speed;
    }

    private void OnMove(InputValue inputVal)
    {
        // Get player movement input (wasd and arrow keys)
        _movementInput = inputVal.Get<Vector2>();
    }
}
