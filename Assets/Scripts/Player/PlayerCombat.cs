using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
	public Animator animator;
	public Transform attackPoint;
	public float attackDistance = 1f;
	public LayerMask enemyLayers;

	// Определение частоты атаки.
	public float attackRate = 2f;
	float nextAttackTime = 0f;

	void Update()
	{
		// Условие на проверку частоты атаки.
		if (Time.time >= nextAttackTime)
			if (Input.GetKeyDown(KeyCode.Space))
			{
				nextAttackTime = Time.time + 1f / attackRate;
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
			enemy.GetComponent<Enemy>().TakeDamage(50);
		}
	}

	private void OnDrawGizmosSelected()
	{
		if (attackPoint == null)
			return;

		Gizmos.DrawWireSphere(attackPoint.position, attackDistance);
	}
}
