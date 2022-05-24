using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
	public Transform attackPoint;
	public float attackRadius;
	public float attackRate;
	public float damage;

	// TODO: Изменить на Entity
	private PlayerController entity;
	private AnimationController animController;
	private float timer = 1f;

	// Start is called before the first frame update
	void Start()
	{
		entity = GetComponent<PlayerController>();
		animController = GetComponent<AnimationController>();	
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
		animController.SetCharacterState("sword_strike", 1f);

		if (enemies != null && enemies.Length != 0)
		{
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
