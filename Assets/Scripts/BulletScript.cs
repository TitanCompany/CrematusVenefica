using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider != GameObject.FindGameObjectWithTag("Player").GetComponent<BoxCollider2D>())
        {
            if(collision.transform.tag == "Mob")
            {
                var enemy = collision.gameObject.GetComponent<Enemy>();
                enemy.TakeDamage(20);
                Destroy(gameObject);
            }
            Destroy(gameObject, 2f);
        }
    }
}
