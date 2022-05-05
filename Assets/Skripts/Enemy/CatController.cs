using Pathfinding;
using Spine.Unity;
using UnityEngine;

public class CatController : Enemy
{
	public SkeletonAnimation skeletonAnimation;
	public AnimationReferenceAsset idle, run, damage, hit, death, newCatRun;
	public string currentState;
	public float speed, dashSpeed;
	private Rigidbody2D rigidbody;
	public string currentAnimation;
	private Vector3 characterScale;
	Vector2 movement;
	public string previousState;

	// Для AStar-Pathfinder
	private AIPath aiPath;
	private AIDestinationSetter aiTarget;

	public Transform searchPoint;
	public float searchDistance = 12f;

	// Для атаки
	public Transform attackPoint;
	public float attackDistance = 2f;
	public LayerMask layerPlayer;
	// Определение частоты атаки.
	public float attackRate = 2f;
	float nextAttackTime = 0f;


	Transform enemyTransform;

	// Для получения урона
	public override int MaxHP { get; set; }
	public override int CurrentHP { get; set; }
	public override bool IsDie { get; set; }

	void Start()
	{
		rigidbody = GetComponent<Rigidbody2D>();
		characterScale = transform.localScale;
		currentState = "Idle";
		SetCharacterState(currentState);
		aiPath = GetComponent<AIPath>();
		aiTarget = GetComponent<AIDestinationSetter>();
		enemyTransform = GetComponent<Transform>();
		layerPlayer = LayerMask.GetMask("Player");

		MaxHP = 50;
		CurrentHP = MaxHP;
	}

	void Update()
	{
		InvokeRepeating("SearchPath", 1f, .5f);
		if (aiPath.desiredVelocity.x >= .01f)
			enemyTransform.localScale = new Vector3(2f, 2f, 2f);
		else if (aiPath.desiredVelocity.x <= -.01f)
			enemyTransform.localScale = new Vector3(-2f, 2f, 2f);

		// Attack
		Collider2D player = Physics2D.OverlapCircle(attackPoint.position, attackDistance, layerPlayer);
		if (player != null && Time.time >= nextAttackTime)
		{
			Attack(player);
			nextAttackTime = Time.time + 3f / attackRate;
		}
	}

	void SearchPath()
	{
		SearchPathToPlayer(aiTarget, searchPoint, searchDistance);
		if (aiTarget != null)
			SetCharacterState("Run");
		else SetCharacterState("Idle");
	}

	#region Animation
	public void SetCharacterState(string state)
	{

		if (state.Equals("Run"))
		{
			SetAnimation(run, true, 2f);
		}
		else if (state.Equals("Death"))
		{
			SetAnimation(death, false, 1f);
		}
		else if (state.Equals("Hit"))
		{
			SetAnimation(hit, false, 2f);
		}
		else
		{
			SetAnimation(idle, true, 1f);
		}

		currentState = state;
	}

	public void SetAnimation(AnimationReferenceAsset animation, bool loop, float timeScale)
	{
		if (animation.name.Equals(currentAnimation))
		{
			return;
		}
		Spine.TrackEntry animationEntry = skeletonAnimation.state.SetAnimation(0, animation, loop);
		animationEntry.TimeScale = timeScale;
		animationEntry.Complete += AnimationEntry_Complete;
		currentAnimation = animation.name;
	}

	private void AnimationEntry_Complete(Spine.TrackEntry trackEntry)
	{
		if (currentState.Equals("Dash"))
		{
			SetCharacterState(previousState);
		}
	}
	#endregion

	public override void TakeDamage(int damage)
	{
		if (!IsDie)
		{
			CurrentHP -= damage;

			if (CurrentHP <= 0)
				Die();
		}
	}

	public override void Attack(Collider2D player)
	{
		player.GetComponent<PlayerController>().GetDamage(Damage);
	}
	public override void Die() 
	{
		IsDie = true;
		SetCharacterState("Death");
	}

	private void OnDrawGizmosSelected()
	{
		if (searchPoint != null)
			Gizmos.DrawWireSphere(searchPoint.position, searchDistance);

		if (attackPoint != null)
			Gizmos.DrawWireSphere(attackPoint.position, attackDistance);
	}
}
