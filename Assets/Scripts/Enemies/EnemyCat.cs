using Pathfinding;
using UnityEngine;

public class EnemyCat : Enemy
{
	public override int MaxHP { get; set; }
	public override int CurrentHP { get; set; }
	public override bool IsDie { get; set; }

    Transform trform;

	public Transform searchPoint;
	public float searchDistance = 10f;
	public LayerMask layerPlayer;
	public Animator animator;
	private SpriteRenderer spriteRender;
	private float vAxis;
	private float hAxis;

	public Transform attackPoint;
	public float attackDistance;

	AIDestinationSetter ai;

	// Определение частоты атаки.
	public float attackRate = 2f;
	float nextAttackTime = 0f;


	void Start()
	{
		trform = GetComponent<Transform>();
		ai = GetComponent<AIDestinationSetter>();
		spriteRender = GetComponent<SpriteRenderer>();
		MaxHP = 100;
		CurrentHP = MaxHP;
		IsDie = false;

		attackDistance = 4;

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
		animator.SetBool("isDead", true);
		GetComponent<Collider2D>().enabled = false;
		Destroy(gameObject, 1.55f);
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

	private void ChangeDirection(bool right)
	{
		if (right)
		{
			spriteRender.flipX = true;
		}
		else
		{
			spriteRender.flipX = false;
		}
	}
}
