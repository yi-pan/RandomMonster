using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAwarenessController : MonoBehaviour
{
    private GameController _gameController;
    public bool AwareOfPlayer { get; private set; }
    public Vector2 DirectiontoPlayer { get; private set; }

    private float _playerAwarenessDistance;

    private Transform _player;
    private void Start()
    {
        _gameController = GameObject.Find("GameController").GetComponent<GameController>();
        _playerAwarenessDistance = _gameController.playerAwarenessDistance;
    }
    private void Awake()
    {
        _player = FindObjectOfType<PlayerMovement>().transform;
    }

    private void Update()
    {
        Vector2 enemyToPlayerVector = _player.position - transform.position;
        DirectiontoPlayer = enemyToPlayerVector.normalized;

        if (enemyToPlayerVector.magnitude <= _playerAwarenessDistance)
        {
            AwareOfPlayer = true;
        }
        else
        {
            AwareOfPlayer = false;
        }
    }
}


