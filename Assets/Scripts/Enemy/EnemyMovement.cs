using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Timeline;
using static UnityEngine.GraphicsBuffer;

public class EnemyMovement : MonoBehaviour
{
    private GameController _gameController;
    private float _speed;
    private float _rotationSpeed;
    private bool isFlipped = false; // track whether the enemy is flipped. default is toward right.

    private Rigidbody2D _rigidbody;
    private PlayerAwarenessController _controller;
    private Vector2 _targetDirection = Vector2.zero;

    private void Start()
    {
        _gameController = GameObject.Find("GameController").GetComponent<GameController>();
        _speed = _gameController.enemyASpeed;
        _rotationSpeed = _gameController.enemyARotationSpeed;
    }
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _controller = GetComponent<PlayerAwarenessController>();
    }

    private void FixedUpdate()
    {
        if (!this.GetComponent<EnemyAttack>().isDead)
        {
            //transform.eulerAngles = Vector3.zero;
            UpdateTargetDirection();
            FlipTowardTarget();
            //RotateTowardTarget();
            SetVelocity();
        }
    }

    private void UpdateTargetDirection()
    {
        if (_controller.AwareOfPlayer)
        {
            _targetDirection = _controller.DirectiontoPlayer;
        }
        else
        {
            _targetDirection = Vector2.zero;
        }
    }

    private void FlipTowardTarget()
    {
        if (_targetDirection == Vector2.zero)
        {
            return;
        }
        else
        {
            if (_targetDirection.x >= 0 && isFlipped)
            {
                Flip();
            }
            else if (_targetDirection.x < 0 && !isFlipped)
            {
                Flip();
            }
        }
    }

    private void Flip()
    {
        // Flip the GameObject by inverting the X scale
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;

        // Toggle the isFlipped flag
        isFlipped = !isFlipped;
    }

    private void RotateTowardTarget()
    {
        if (_targetDirection == Vector2.zero)
        {
            return;
        }
        else
        {
            Quaternion targetRotation = Quaternion.LookRotation(transform.forward, _targetDirection);
            Quaternion rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);

            _rigidbody.SetRotation(rotation);
        }
    }

    private void SetVelocity()
    {
        if (_targetDirection == Vector2.zero)
        {
            _rigidbody.velocity = Vector2.zero;
        }
        else
        {

            _rigidbody.velocity = _targetDirection * _speed;
        }
    }
}
