using Pathfinding;
using UnityEngine;

public class EnemyCat : Enemy
{
	public override int MaxHP { get; set; }
	public override int CurrentHP { get; set; }
	public override bool IsDie { get; set; }

	Transform trform;

	public Transform searchPoint;
	public float searchDistance = 25f;
	public LayerMask layerPlayer;

	AIDestinationSetter ai;
	Transform playerTr;
	Player player;


	void Start()
	{
		trform = GetComponent<Transform>();
		ai = GetComponent<AIDestinationSetter>();
		player = GetComponent<Player>();
		MaxHP = 100;
		CurrentHP = MaxHP;
		IsDie = false;
	}

	void Update()
	{
		ai.target = player.transform; 
		//SearchPathToPlayer();
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
