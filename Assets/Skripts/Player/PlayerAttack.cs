using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
	public Transform attackPoint;
	public float attackRadius;
	public float attackRate;
	public float damage;

	// TODO: Изменить на Entity
	private PlayerController entity;
	private float timer = 1;

	// Start is called before the first frame update
	void Start()
	{
		entity = GetComponent<PlayerController>();
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKey(KeyCode.Space) && timer >= 1f / attackRate)
			Attack();
		timer += Time.deltaTime;

	}

	private void Attack()
	{
		Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, LayerMask.GetMask("Enemy"));
		if (enemies != null && enemies.Length != 0)
		{
			entity.animController.SetCharacterState("sword_strike");
			foreach (var enemy in enemies)
			{
				var entity = enemy.GetComponent<Entity>();
				entity.TakeDamage(damage);
			}
			timer = 0;
		}
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
	}
}
