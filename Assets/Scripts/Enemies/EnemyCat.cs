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

	AIDestinationSetter ai;

	void Start()
	{
		trform = GetComponent<Transform>();
		ai = GetComponent<AIDestinationSetter>();
		MaxHP = 100;
		CurrentHP = MaxHP;
		IsDie = false;
		layerPlayer = LayerMask.GetMask("Player");
	}

	void Update()
	{
		SearchPathToPlayer(ai, searchPoint, searchDistance);
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
		trform.Rotate(0f, 0f, 45f);
		GetComponent<Collider2D>().enabled = false;
		trform.position = new Vector3(trform.position.x, trform.position.y, 1f);
		Destroy(gameObject, 6f);
	}

	private void OnDrawGizmosSelected()
	{
		if (searchPoint == null)
			return;

		Gizmos.DrawWireSphere(searchPoint.position, searchDistance);
	}

	public void SearchPathToPlayer()
	{
		Collider2D player = Physics2D.OverlapCircle(searchPoint.position, searchDistance, layerPlayer);
		if (player != null && ai.target == null)
		{
			ai.target = GameObject.FindGameObjectsWithTag("Player")[0].transform;
		}
		else if (player == null)
			ai.target = null;
	}
}
