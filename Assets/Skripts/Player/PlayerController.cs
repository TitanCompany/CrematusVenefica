using Spine.Unity;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	// Все для анимации
	public SkeletonAnimation skeletonAnimation;
	public AnimationReferenceAsset idle, walking, dash, sword, archery;
	public AnimationReferenceAsset[] anims;
	public Entity entity;
	public PlayerAttack playerAttack;
	public int maxRoots;
	public int numRoots;

	internal AnimationController animController;

	private Level playerLevel;

	//private Rigidbody2D _rigidbody;
	//public string currentAnimation;
	//private Vector3 characterScale;
	/*public string previousState;
	public string currentState;*/

	/*// Все для атаки ближнего боя
	public Transform attackPoint;
	public float attackDistance = 1f;
	public LayerMask enemyLayers;*/

	// Все для передвижения
	//public float speed, dashSpeed;
	//Vector2 movement;

	// Все для получения урона
	//public float MaxHealthPoints;
	//public float currentHealthPoints;

	void Start()
	{
		animController = new AnimationController(
			skeletonAnimation,
			new AnimationController.Animation(idle, true, 1f),
			new AnimationController.Animation(walking, true, 1f),
			new AnimationController.Animation(dash, false, 2f),
			new AnimationController.Animation(sword, false, 2f),
			new AnimationController.Animation(archery, false, 1f)
			);
		animController.SetSpecialAnims("dash");
		animController.SetCharacterState("idle");
		entity = GetComponent<Entity>();
		playerAttack = GetComponent<PlayerAttack>();
		numRoots = maxRoots-1;


		//_rigidbody = GetComponent<Rigidbody2D>();
		//characterScale = transform.localScale;
		//MaxHealthPoints = 100;
		//currentHealthPoints = MaxHealthPoints;
	}

	float timer = 0;
	void Update()
	{
		if (Input.GetKey(KeyCode.Tab) && timer > 1f)
			ChangeAnimMode(ref timer);
		if (Input.GetKey(KeyCode.Q) && timer > 1f)
			Heal(ref timer);
		if (Input.GetKey(KeyCode.F5) && timer > 1f)
			Save();
		if (Input.GetKey(KeyCode.F6) && timer > 1f)
			Load();
		timer += Time.deltaTime;
	}

	// Изменяет скин игрока.
	void ChangeAnimMode(ref float timer)
	{
		if (animController.skeletonAnimation.skeleton.Skin.ToString() == "swordsman")
			animController.skeletonAnimation.Skeleton.SetSkin("archer");
		else
			animController.skeletonAnimation.Skeleton.SetSkin("swordsman");

		timer = 0;
	}

	void Heal(ref float timer)
    {
		if (entity.currentHP == entity.maxHP)
			return;
		if (numRoots == 0)
			return;
		entity.currentHP += entity.maxHP*0.20f;
		if (entity.currentHP > entity.maxHP)
			entity.currentHP = entity.maxHP;
		numRoots -= 1;
		timer = 0;
	}

	public void Save()
    {
		SaveLoadSystem.Save(this);
    }

	public void Load()
    {
		PlayerData data = SaveLoadSystem.Load();
		entity.maxHP = data.maxHP;
		entity.currentHP = data.currentHP;
		maxRoots = data.maxRoots;
		numRoots = data.numRoots;
		playerAttack.damage = data.damage;
		Vector3 position;
		position.x = data.position[0];
		position.y = data.position[1];
		position.z = data.position[2];
		transform.position = position;
	}

	/*#region Animation
	// Set Character animation
	public void SetAnimation(AnimationReferenceAsset animation, bool loop, float timeScale)
	{
		if (animation.name == currentAnimation)
			return;
		Spine.TrackEntry animationEntry = skeletonAnimation.state.SetAnimation(0, animation, loop);
		animationEntry.TimeScale = timeScale;
		animationEntry.Complete += AnimationEntry_Complete;
		currentAnimation = animation.name;
	}

	// Do something after animation completes
	private void AnimationEntry_Complete(Spine.TrackEntry trackEntry)
	{
		if (currentState.Equals("Dash"))
		{
			SetCharacterState(previousState);
		}
	}

	// Check character state and sets the animation accordingly
	public void SetCharacterState(string state)
	{

		if (state.Equals("Walking"))
		{
			SetAnimation(walking, true, 2f);
		}
		else if (state.Equals("Dash"))
		{
			SetAnimation(dash, false, 2f);
		}
		else if (state.Equals("Hit"))
		{
			if (skeletonAnimation.skeleton.Skin.ToString() == "swordsman")
				SetAnimation(sword_strike, false, 2f);
			else SetAnimation(archery, false, 2f);
		}
		else
		{
			SetAnimation(idle, true, 1f);
		}

		currentState = state;
	}
	#endregion*/

	/*#region Move
	public void Move()
	{
		movement.x = Input.GetAxisRaw("Horizontal");
		movement.y = Input.GetAxisRaw("Vertical");

		_rigidbody.MovePosition(_rigidbody.position + movement * speed * Time.fixedDeltaTime);

		if (movement.x != 0 || movement.y != 0)
		{
			if (!currentState.Equals("dash"))
			{
				animController.SetCharacterState("run");
			}

			if (movement.x > 0)
			{
				transform.localScale = new Vector2(characterScale.x, characterScale.y);
			}
			else
			{
				transform.localScale = new Vector2(-characterScale.x, characterScale.y);
			}
		}
		else
		{
			if (!currentState.Equals("dash"))
			{
				animController.SetCharacterState("idle");
			}

		}

		if (Input.GetButtonDown("Jump")) Dash();


		// Skin change example
		if (Input.GetButtonDown("Fire3"))
		{
			if (skeletonAnimation.skeleton.Skin.ToString() == "swordsman")
				skeletonAnimation.Skeleton.SetSkin("archer");
			else
				skeletonAnimation.Skeleton.SetSkin("swordsman");
			skeletonAnimation.Skeleton.SetSlotsToSetupPose();
		}

		if (Input.GetButtonDown("Fire1"))
		{
			//SetCharacterState("Hit");
			if (skeletonAnimation.skeleton.Skin.ToString() == "swordsman")
				SwordAttack();
			else BowAttack();
		}
	}

	public void Dash()
	{
		//_rigidbody.velocity = new Vector2(_rigidbody.velocity.x, dashSpeed);
		if (!currentState.Equals("dash"))
		{
			previousState = currentState;
		}
		animController.SetCharacterState("dash");
	}
	#endregion

	#region Attack
	public void BowAttack()
	{

	}

	public void SwordAttack()
	{
		// Search Enemy
		Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackDistance, enemyLayers);

		// Damage to Enemy
		foreach (var enemy in hitEnemies)
		{
			Entity en = enemy.GetComponent<Entity>();
			en.TakeDamage(20f);
			print(en.maxHP);
		}
	}

	private void OnDrawGizmosSelected()
	{
		if (attackPoint == null)
			return;

		Gizmos.DrawWireSphere(attackPoint.position, attackDistance);
	}
	#endregion

	#region Damage
	public void GetDamage(float damage)
	{
		currentHealthPoints -= damage;
		print(currentHealthPoints);

		//Text HPBar = GameObject.FindGameObjectWithTag("HPBar").GetComponent<Text>();
		//HPBar.text = $"HP {currentHealthPoints}/100";

		if (currentHealthPoints <= 0)
			Die();
	}

	public void Die()
	{
		Destroy(gameObject, 0.9f);
		print("YOU DEAD");
	}
	#endregion*/
}
