using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public bool isDead = false;
    private float _damage;
    private GameController _gameController;
    private Animator animator;

    private void Start()
    {
        _gameController = GameObject.Find("GameController").GetComponent<GameController>();
        _damage = _gameController.enemyADamage;

        //get animator
        animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isDead)
        {
            if (collision.gameObject.GetComponent<PlayerMovement>())
            {
                var healthController = collision.gameObject.GetComponent<HealthController>();
                healthController.TakeDamage(_damage);

                //attack
                animator.SetBool("isAttacking", true);
                // Debug.Log("Animation is true");
            }
        }
    }
}
