
using Pathfinding;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	public Transform searchPoint;
	public float searchRadius;

	public int experience;

	private AIDestinationSetter ai;
	private AIPath aiPath;
	private Entity entity;

	private bool needSearchPath = true;

	private void Start()
	{
		ai = GetComponent<AIDestinationSetter>();
		aiPath = GetComponent<AIPath>();
		entity = GetComponent<Entity>();
	}

	void Update()
	{
		if (needSearchPath)
		{
			InvokeRepeating("SearchPath", 1f, .5f);
			if (aiPath.desiredVelocity.x >= .01f)
				transform.localScale = new Vector3(2f, 2f, 2f);
			else if (aiPath.desiredVelocity.x <= -.01f)
				transform.localScale = new Vector3(-2f, 2f, 2f);
		}
	}
	void SearchPath()
	{
		var cat = GetComponent<CatController>();
		SearchPathToPlayer();
		if (ai.target == null)
			cat.SetCharacterState("Idle");
		else if (cat != null)
			cat.SetCharacterState("Run");
	}

	

	// Search and go to Player
	public void SearchPathToPlayer()
	{
		Collider2D player = Physics2D.OverlapCircle(searchPoint.position, searchRadius, LayerMask.GetMask("Player"));

		if (player != null && ai.target == null)
			ai.target = GameObject.FindGameObjectWithTag("Player").transform;
		else if (player == null)
			ai.target = null;
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject == GameObject.FindGameObjectWithTag("Player"))
		{
			aiPath.isStopped = true;
			needSearchPath = false;
		}
		else
			needSearchPath = true;
	}

	private void OnCollisionExit2D(Collision2D collision)
	{
		if (collision.gameObject == GameObject.FindGameObjectWithTag("Player"))
		{
			aiPath.isStopped = false;
			needSearchPath = true;
		}
		else
			needSearchPath = false;
	}

	private void OnDrawGizmosSelected()
	{
		if (searchPoint != null)
			Gizmos.DrawWireSphere(searchPoint.position, searchRadius);
	}
}