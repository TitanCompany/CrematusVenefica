using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Spine.Unity;

public class PlayerController: MonoBehaviour
{
    // Все для анимации
	public SkeletonAnimation skeletonAnimation;
    public AnimationReferenceAsset idle, walking, dash, hit;
	private Rigidbody2D rigidbody;
    public string currentAnimation;
    private Vector3 characterScale;
	public string previousState;
	public string currentState;
	
	// Все для атаки ближнего боя
	public Transform attackPoint;
	public float attackDistance = 1f;
	public LayerMask enemyLayers;
	
    // Все для передвижения
	public float speed, dashSpeed;
    Vector2 movement;

	// Все для получения урона
	public float MaxHealthPoints;
	public float currentHealthPoints;

	void Start()
    {
		rigidbody = GetComponent<Rigidbody2D>();
		characterScale = transform.localScale; 
		currentState = "Idle";
		SetCharacterState(currentState);
		MaxHealthPoints = 100;
		currentHealthPoints = MaxHealthPoints;
	}

    void Update()
    {
        Move();
    }


    #region Animation
    // Set Character animation
    public void SetAnimation(AnimationReferenceAsset animation, bool loop, float timeScale)
    {
		if(animation.name.Equals(currentAnimation))
		{
			return;
		}
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

		if(state.Equals("Walking"))
		{
			SetAnimation(walking, true, 2f);
		}
		else if (state.Equals("Dash"))
        {
			SetAnimation(dash, false, 2f);
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
    #endregion

    #region Move
    public void Move()
    {
		movement.x = Input.GetAxisRaw("Horizontal");
		movement.y = Input.GetAxisRaw("Vertical");

		rigidbody.MovePosition(rigidbody.position + movement * speed * Time.fixedDeltaTime);

		if(movement.x != 0 || movement.y != 0)
		{
            if (!currentState.Equals("Dash"))
            {
				SetCharacterState("Walking");
            }
			
			if(movement.x > 0)
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
			if (!currentState.Equals("Dash"))
            {
				SetCharacterState("Idle");
            }
				
		}

		if (Input.GetButtonDown("Jump")) Dash();


		// Skin change example
		if (Input.GetButtonDown("Fire3"))
		{
			if(skeletonAnimation.skeleton.Skin.ToString() == "swordsman")
				skeletonAnimation.Skeleton.SetSkin("archer");
			else
				skeletonAnimation.Skeleton.SetSkin("swordsman");
			skeletonAnimation.Skeleton.SetSlotsToSetupPose();
		}

        if (Input.GetButtonDown("Fire1"))
        {
			SetCharacterState("Hit");
        }

	}

	public void Dash()
    {
		rigidbody.velocity =  new Vector2(rigidbody.velocity.x, dashSpeed);
        if (!currentState.Equals("Dash"))
        {
			previousState = currentState;
        }
		SetCharacterState("Dash");
    }
    #endregion

    #region Attack
    public void Attack()
	{
		// Search Enemy
		Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackDistance, enemyLayers);

		// Damage to Enemy
		foreach (var enemy in hitEnemies)
		{
			Debug.Log("Attack succsess to " + enemy.name);
			enemy.GetComponent<Enemy>().TakeDamage(20);
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

		Text HPBar = GameObject.FindGameObjectWithTag("HPBar").GetComponent<Text>();
		HPBar.text = $"HP {currentHealthPoints}/100";

		if (currentHealthPoints <= 0)
			Die();
	}

	public void Die()
	{
		Destroy(gameObject, 0.9f);
		print("YOU DEAD");
	}
    #endregion

}
