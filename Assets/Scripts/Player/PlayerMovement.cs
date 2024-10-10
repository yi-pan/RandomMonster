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
    private Camera _camera;

    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _rotationSpeed;
    [SerializeField]
    private float _smoothTime = 0.1f;
    [SerializeField]
    private float _screenBorder;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _camera = Camera.main;
    }

    private void FixedUpdate()
    {
        SetPlayerVelocity();
        SetPlayerRotation();
    }

    private void SetPlayerRotation()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector2 direction = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);

        transform.right = direction;
    }

    private void SetPlayerVelocity()
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
        KeepPlayeronScreen();
    }

    private void KeepPlayeronScreen()
    {
        Vector2 screenPosition = _camera.WorldToScreenPoint(transform.position);
        if ((screenPosition.x < _screenBorder && _rigidbody.velocity.x < 0) || (screenPosition.x > _camera.pixelWidth - _screenBorder && _rigidbody.velocity.x > 0))
        {
            _rigidbody.velocity = new Vector2(0, _rigidbody.velocity.y);
        }
        if ((screenPosition.y < _screenBorder && _rigidbody.velocity.y < 0) || (screenPosition.y > _camera.pixelHeight - _screenBorder && _rigidbody.velocity.y > 0))
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0);
        }
    }
    private void OnMove(InputValue inputVal)
    {
        // Get player movement input (wasd and arrow keys)
        _movementInput = inputVal.Get<Vector2>();
    }
}
