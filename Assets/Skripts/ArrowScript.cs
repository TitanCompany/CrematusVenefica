using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        gameObject.transform.Rotate(0f, 0f, 90f);
        if(collision.collider != GameObject.FindGameObjectWithTag("Player").GetComponent<BoxCollider2D>())
        {
            if(collision.transform.tag == "Enemy")
            {
                var enemy = collision.gameObject.GetComponent<Entity>();
                enemy.TakeDamage(20);
                Destroy(gameObject);
            }
        }
        Destroy(gameObject, 2f);
    }
}
