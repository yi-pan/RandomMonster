using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Events;
using TMPro;
using System;

public class HealthController : MonoBehaviour
{
    private GameController _gameController;

    private float _health;
    private float _maxHealth;

    [SerializeField]
    private GameObject _healthUIText;
    private void Start()
    {
        // Find the GameController object and get the GameController script
        _gameController = GameObject.Find("GameController").GetComponent<GameController>();
        _health = _gameController.playerMaxHealth;
        _maxHealth = _gameController.playerMaxHealth;
        // change UI
        string healthString = _health.ToString();
        _healthUIText.GetComponent<TMP_Text>().text = healthString;
    }
    public float RemainingHealthPercentage
    {
        get
        {
            return _health / _maxHealth;
        }
    }

    public bool IsInvincible { get; set; }

    public UnityEvent OnPlayerDie;
    public UnityEvent OnPlayerDamaged;
    public UnityEvent OnHealthChanged;
    public void TakeDamage(float damage)
    {
        if (IsInvincible) { 
            return; 
        }

        if (_health - damage > 0)
        {
            _health -= damage;
        }
        else
        {
            _health = 0;
        }

        if (_health == 0)
        {
            OnPlayerDie.Invoke();
        }
        else
        {
            OnPlayerDamaged.Invoke();
        }

        OnHealthChanged.Invoke();
        // change UI
        string healthString = _health.ToString();
        _healthUIText.GetComponent<TMP_Text>().text = healthString;
        // update player current health in GameController
        _gameController.playerCurrentHealth = _health;
    }

    public void AddHealth(float healthToAdd)
    {
        if (_health == _maxHealth)
        {
            return;
        }

        OnHealthChanged.Invoke();


        if (_health + healthToAdd > _maxHealth)
        {
            _health = _maxHealth;
        }
        else
        {
            _health += healthToAdd;
        }

    }
}
