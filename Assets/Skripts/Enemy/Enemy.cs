
using Pathfinding;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
	public abstract int MaxHP { get; set; }
	public abstract int CurrentHP { get; set; }
	public abstract bool IsDie { get; set; }
	public float Damage;

	public abstract void TakeDamage(int damage);
	public abstract void Attack(Collider2D player);
	public abstract void Die();

	private void Update()
	{ }

	private void Start()
	{
	}

	// Search and go to Player
	public void SearchPathToPlayer(AIDestinationSetter ai, Transform searchPoint, float radius)
	{
		Collider2D player = Physics2D.OverlapCircle(searchPoint.position, radius, LayerMask.GetMask("Player"));

		if (player != null && ai.target == null)
			ai.target = GameObject.FindGameObjectWithTag("Player").transform;
		else if (player == null)
			ai.target = null;
	}
}