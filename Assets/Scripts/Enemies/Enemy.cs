using Pathfinding;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
	public abstract int MaxHP { get; set; }
	public abstract int CurrentHP { get; set; }
	public abstract bool IsDie { get; set; }
	public abstract float Damage { get; set;}

	public abstract void TakeDamage(int damage);
	public abstract void Attack(Collider2D player);
	public abstract void Die();

	Transform enemyTransform;
	SpriteRenderer enemySprite;

	// Search and go to Player
	public void SearchPathToPlayer(AIDestinationSetter ai, Transform searchPoint, float radius)
	{
		Collider2D player = Physics2D.OverlapCircle(searchPoint.position, radius, LayerMask.GetMask("Player"));

		if (player != null && ai.target == null)
			ai.target = GameObject.FindGameObjectsWithTag("Player")[0].transform;
		else if (player == null)
			ai.target = null;

		if (enemyTransform == null || enemySprite == null)
		{
			enemyTransform = GetComponent<Transform>();
			enemySprite = GetComponent<SpriteRenderer>();
		}

		if (player != null)
		{
			if (enemyTransform.position.x < player.transform.position.x)
				enemySprite.flipX = false;
			else if (enemyTransform.position.x > player.transform.position.x)
				enemySprite.flipX = true;
		}
	}
}
