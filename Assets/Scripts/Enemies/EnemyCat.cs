using Pathfinding;
using UnityEngine;

public class EnemyCat : Enemy
{
	public override int MaxHP { get; set; }
	public override int CurrentHP { get; set; }
	public override bool IsDie { get; set; }
    public override float Damage { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    Transform trform;

	public Transform searchPoint;
	public float searchDistance = 10f;
	public LayerMask layerPlayer;
	public Animator animator;
	private SpriteRenderer spriteRender;
	private float vAxis;
	private float hAxis;

	AIDestinationSetter ai;

	void Start()
	{
		trform = GetComponent<Transform>();
		ai = GetComponent<AIDestinationSetter>();
		spriteRender = GetComponent<SpriteRenderer>();
		MaxHP = 100;
		CurrentHP = MaxHP;
		IsDie = false;
		layerPlayer = LayerMask.GetMask("Player");
	}

	void Update()
	{
		SearchPathToPlayer(ai, searchPoint, searchDistance);
		vAxis = 0;
		float isMove = Mathf.Abs(vAxis) + Mathf.Abs(hAxis);
		animator.SetFloat("catSpeed", isMove);

		if (hAxis != 0 || vAxis != 0)
		{
			if (hAxis > 0)
			{
				ChangeDirection(false);
			}
			else if (hAxis < 0)
			{
				ChangeDirection(true);
			}
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

    public override void Attack(Collider2D player)
    {
        throw new System.NotImplementedException();
    }
}
