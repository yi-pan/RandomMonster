using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Bullet : MonoBehaviour
{
    private bool visible = true;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        // bullet hit enemy
        if (collision.gameObject.GetComponent<EnemyMovement>())
        {
            Animator animator = collision.gameObject.GetComponent<Animator>();
            animator.SetBool("isDead", true);

            //destroy enemy
            Destroy(collision.gameObject, 2f);

            Destroy(gameObject);
        }

        // bullet hit obstacle
        if (collision.gameObject.GetComponent<Tilemap>())
        {
            Destroy(gameObject);
        }
    }

    void OnBecameInvisible()
    {
        visible = false;
    }

    public void Update()
    {
        if (!visible)
        {
            Destroy(gameObject);
        }
    }
}
