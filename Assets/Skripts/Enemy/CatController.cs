using Pathfinding;
using Spine.Unity;
using UnityEngine;

public class CatController : Enemy
{
	public SkeletonAnimation skeletonAnimation;
	public AnimationReferenceAsset idle, run;
	public string currentState;
	public float speed, dashSpeed;
	private Rigidbody2D rigidbody;
	public string currentAnimation;
	private Vector3 characterScale;
	Vector2 movement;
	public string previousState;

	// Äëÿ AStar-Path
	private AIPath aiPath;
	private AIDestinationSetter aiTarget;

	public Transform searchPoint;
	public float searchDistance = 12f;

	public Transform attackPoint;
	public float attackDistance = 2f;

	Transform enemyTransform;

	public override int MaxHP { get; set; }
	public override int CurrentHP { get; set; }
	public override bool IsDie { get; set; }

	// Start is called before the first frame update
	void Start()
	{
		rigidbody = GetComponent<Rigidbody2D>();
		characterScale = transform.localScale;
		currentState = "Idle";
		SetCharacterState(currentState);
		aiPath = GetComponent<AIPath>();
		aiTarget = GetComponent<AIDestinationSetter>();
		enemyTransform = GetComponent<Transform>();
	}

	// Update is called once per frame
	void Update()
	{
		InvokeRepeating("SearchPath", 1f, .5f);
		if (aiPath.desiredVelocity.x >= .01f)
			enemyTransform.localScale = new Vector3(2f, 2f, 2f);
		else if (aiPath.desiredVelocity.x <= -.01f)
			enemyTransform.localScale = new Vector3(-2f, 2f, 2f);
	}

	void SearchPath()
	{
		SearchPathToPlayer(aiTarget, searchPoint, searchDistance);
	}

	#region Animation
	public void SetCharacterState(string state)
	{

		if (state.Equals("Run"))
		{
			SetAnimation(run, true, 2f);
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

	}
	public override void Attack(Collider2D player) { }
	public override void Die() { }

	private void OnDrawGizmosSelected()
	{
		if (searchPoint != null)
			Gizmos.DrawWireSphere(searchPoint.position, searchDistance);

		if (attackPoint != null)
			Gizmos.DrawWireSphere(attackPoint.position, attackDistance);
	}
}
