using Pathfinding;
using Spine.Unity;
using UnityEngine;

public class CatController : MonoBehaviour
{
	public SkeletonAnimation skeletonAnimation;
	public AnimationReferenceAsset idle, run, damage, hit, death, newCatRun;

	public string currentState;
	public float speed, dashSpeed;
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

	Entity entity;
	EntityAttack entityAttack;

	Transform enemyTransform;

	void Start()
	{
		characterScale = transform.localScale;
		currentState = "Idle";
		entity = GetComponent<Entity>();
		entityAttack = GetComponent<EntityAttack>();
		SetCharacterState(currentState);
		aiPath = GetComponent<AIPath>();
		aiTarget = GetComponent<AIDestinationSetter>();
		enemyTransform = GetComponent<Transform>();
		layerPlayer = LayerMask.GetMask("Player");
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
		else if (state.Equals("Damage"))
			SetAnimation(damage, false, 1f);
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

	private void OnDrawGizmosSelected()
	{
		if (searchPoint != null)
			Gizmos.DrawWireSphere(searchPoint.position, searchDistance);

		if (attackPoint != null)
			Gizmos.DrawWireSphere(attackPoint.position, attackDistance);
	}
}
