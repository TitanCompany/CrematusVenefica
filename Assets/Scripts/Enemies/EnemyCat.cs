using UnityEngine;
using UnityEngine.AI;

public class EnemyCat : Enemy
{
	public override int MaxHP { get; set; }
	public override int CurrentHP { get; set; }
	public override bool IsDie { get; set; }

	Transform trform;
	NavMeshAgent agent;

	public Transform searchPoint;
	public float searchDistance = 25f;
	public LayerMask layerPlayer;


	void Start()
	{
		trform = GetComponent<Transform>();
		agent = GetComponent<NavMeshAgent>();
		MaxHP = 100;
		CurrentHP = MaxHP;
		IsDie = false;

	}

	void Update()
	{
		GoToPlayer();
	}

	private void GoToPlayer()
	{
		Collider2D player = Physics2D.OverlapCircle(searchPoint.position, searchDistance, layerPlayer);
		//agent.SetDestination(player.ClosestPoint);
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
		// TODO: Enemy is disable
		GetComponent<Collider2D>().enabled = false;
		trform.position = new Vector3(trform.position.x, trform.position.y, 1f);
		this.enabled = false;
	}

	private void OnDrawGizmosSelected()
	{
		if (searchPoint == null)
			return;

		Gizmos.DrawWireSphere(searchPoint.position, searchDistance);
	}
}
