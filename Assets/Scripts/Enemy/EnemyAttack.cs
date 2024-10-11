using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    
    private float _damage;
    private GameController _gameController;
    private void Start()
    {
        _gameController = GameObject.Find("GameController").GetComponent<GameController>();
        _damage = _gameController.enemyADamage;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerMovement>())
        {
            var healthController = collision.gameObject.GetComponent<HealthController>();
            healthController.TakeDamage(_damage);
        }
    }
}
