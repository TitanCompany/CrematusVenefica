using Pathfinding;
using UnityEngine;

public class EnemyCat : Enemy
{
	public override int MaxHP { get; set; }
	public override int CurrentHP { get; set; }
	public override bool IsDie { get; set; }
	public override float Damage { get; set; }

	Transform catTransform;

	public LayerMask layerPlayer;
	public Transform searchPoint;
	public float searchDistance = 10f;

	public Transform attackPoint;
	public float attackDistance;

	AIDestinationSetter ai;

	// Определение частоты атаки.
	public float attackRate = 2f;
	float nextAttackTime = 0f;


	void Start()
	{
		catTransform = GetComponent<Transform>();
		ai = GetComponent<AIDestinationSetter>();
		MaxHP = 100;
		CurrentHP = MaxHP;
		IsDie = false;

		attackDistance = 4;
		Damage = 3;

		layerPlayer = LayerMask.GetMask("Player");
	}

	void Update()
	{
		SearchPathToPlayer(ai, searchPoint, searchDistance);

		Collider2D player = Physics2D.OverlapCircle(attackPoint.position, attackDistance, layerPlayer);
		if (player != null && Time.time >= nextAttackTime)
		{
			Attack(player);
			nextAttackTime = Time.time + 3f / attackRate;
		}
	}

	public override void TakeDamage(int damage)
	{
		if (!IsDie)
		{
			CurrentHP -= damage;

			if (CurrentHP <= 0)
				Die();
		}
	}

	public override void Die()
	{
		IsDie = true;
		Debug.Log("Enemy is Dead");
		// TODO: Die Animation 
		// animator.SetBool("IsDead", true);
		catTransform.Rotate(0f, 0f, 45f);
		GetComponent<Collider2D>().enabled = false;
		catTransform.position = new Vector3(catTransform.position.x, catTransform.position.y, 1f);
		Destroy(gameObject, 6f);
	}

	private void OnDrawGizmosSelected()
	{
		if (searchPoint != null)
			Gizmos.DrawWireSphere(searchPoint.position, searchDistance);

		if (attackPoint != null)
			Gizmos.DrawWireSphere(attackPoint.position, attackDistance);

	}

	public override void Attack(Collider2D player)
	{
		print("Cat Attack!");
		player.GetComponent<Player>().GetDamage(Damage);
	}
}
