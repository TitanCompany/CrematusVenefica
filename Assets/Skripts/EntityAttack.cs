using UnityEngine;

public class EntityAttack : MonoBehaviour
{
	public float simpleAttackDamage;
	public float simpleAttackRate;
	//public float forceAttackDamage;
	//public float forceAttackRate; 

	public Transform attackPoint;
	public float attackRadius;
	public LayerMask enemyLayer;

	private Entity entity;
	private float timerAttack = 0;

	private void Start()
	{
		entity = GetComponent<Entity>();
	}

	private void Update()
	{
		if (timerAttack > 1f / simpleAttackRate)
		{
			Attack();
		}
		timerAttack += Time.deltaTime;
	}

	private void Attack()
	{
		Collider2D[] enemiesInRadius = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, enemyLayer);
		if (enemiesInRadius != null && enemiesInRadius.Length != 0)
		{
			foreach (var enemy in enemiesInRadius)
			{
				Entity entity = enemy.GetComponent<Entity>();
				entity.TakeDamage(simpleAttackDamage);
			}
			timerAttack = 0;

			// TODO: Temp
			if (gameObject.name != "Player")
				GetComponent<CatController>().SetCharacterState("Hit");

		}
	}

	private void OnDrawGizmosSelected()
	{
		if (attackPoint != null)
			Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
	}
	// Могут быть еще спец приемы и тд.
}
