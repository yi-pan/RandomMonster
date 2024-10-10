using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Events;

public class HealthController : MonoBehaviour
{
    [SerializeField]
    private float _health;

    [SerializeField]
    private float _maxHealth;

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
