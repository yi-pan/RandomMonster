using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Timeline;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField]
    private float _speed;

    [SerializeField]
    private float _rotationSpeed;

    private Rigidbody2D _rigidbody;
    private PlayerAwarenessController _controller;
    private Vector2 _targetDirection = Vector2.zero;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _controller = GetComponent<PlayerAwarenessController>();
    }

    private void FixedUpdate()
    {
        UpdateTargetDirection();
        RotateTowardTarget();
        SetVelocity();
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
            _rigidbody.velocity = transform.up * _speed;
        }
    }
}
