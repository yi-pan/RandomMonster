using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerInvincible : MonoBehaviour
{
    private InvincibleController _invincibleController;
    private GameController _gameController;

    private float _invincibleDuration;

    private void Start()
    {
        _gameController = GameObject.Find("GameController").GetComponent<GameController>();
        _invincibleDuration = _gameController.playerIncinvinbleDuration;
    }
    private void Awake()
    {
        _invincibleController = GetComponent<InvincibleController>();

    }
    public void StartInvincible()
    {
        _invincibleController.StartInvincible(_invincibleDuration);
    }
}
