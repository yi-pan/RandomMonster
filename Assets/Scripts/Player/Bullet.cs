using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Bullet : MonoBehaviour
{
    private bool visible = true;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // bullet hit enemy
        if (collision.gameObject.GetComponent<EnemyMovement>())
        {
            Destroy(collision.gameObject);
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

    private void Update()
    {
        if (!visible)
        {
            Destroy(gameObject);
        }
    }
}
