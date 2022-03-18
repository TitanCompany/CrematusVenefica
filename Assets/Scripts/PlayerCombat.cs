using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Animator animator;
    public Transform attackPoint;
    public float attackDistance = 0.5f;
    public LayerMask enemyLayers;

    void Update()
    {
		if (Input.GetKeyDown(KeyCode.Space))
		{
            Attack();
		}
    }

    void Attack()
	{
        // Play Animation
        animator.SetTrigger("Attack");

        // Search Enemy
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackDistance, enemyLayers);

		// Damage to Enemy
		foreach (var enemy in hitEnemies)
		{
            Debug.Log("Attack succsess to " + enemy.name);
			enemy.GetComponent<Enemy>().TakeDamage(10); 
		}
	}

	private void OnDrawGizmosSelected()
	{
		if (attackPoint == null)
			return;
		
        Gizmos.DrawWireSphere(attackPoint.position, attackDistance);
	}
}
