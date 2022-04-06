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
	float attackDisable = 0f;

	void Update()
	{
		// Условие на проверку частоты атаки.
		if (Time.time >= nextAttackTime)
			if (Input.GetKeyDown(KeyCode.Space))
			{
				nextAttackTime = Time.time + 3f / attackRate;
				animator.SetBool("isAttack", true);
				Attack();
			}
		if(Time.time >= attackDisable)
        {
			animator.SetBool("isAttack", false);
			attackDisable = Time.time + 1.5f;
        }
	}

	void Attack()
	{
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
