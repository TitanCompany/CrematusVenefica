using UnityEngine;

public class PlayerMove : MonoBehaviour
{
	public float MovementSpeed;
	private float vAxis;
	private float hAxis;

	private Rigidbody2D rbody;
	private Animator animator;
	private PlayerCombat playerCombat;
	private SpriteRenderer spriteRender;

	private Transform plTransform;
	private Transform transformAttackPoint;



	void Start()
	{
		rbody = GetComponent<Rigidbody2D>();
		spriteRender = GetComponent<SpriteRenderer>();
		playerCombat = GetComponent<PlayerCombat>();
		animator = GetComponent<Animator>();
		plTransform = GetComponent<Transform>();
		transformAttackPoint = playerCombat.attackPoint.transform;
	}

	private void FixedUpdate()
	{
		hAxis = Input.GetAxis("Horizontal");
		vAxis = Input.GetAxis("Vertical");
		var move = new Vector2(MovementSpeed * hAxis, MovementSpeed * vAxis);
		rbody.velocity = move;
	}

	void Update()
	{
		if (hAxis != 0 || vAxis != 0)
		{
			animator.Play("Player_run");
			if (hAxis > 0)
			{
				ChangeDirection(false);
			}
			else if (hAxis < 0)
			{
				ChangeDirection(true);
			}
		}
		else
			animator.Play("Player_static");
	}

	// Смена направления.
	private void ChangeDirection(bool right)
	{
		if (right)
		{
			spriteRender.flipX = true;
			transformAttackPoint.position = new Vector3(plTransform.position.x + -0.55f, plTransform.position.y + 0.01f, 0);
		}
		else
		{
			spriteRender.flipX = false;
			transformAttackPoint.position = new Vector3(plTransform.position.x + 0.55f, plTransform.position.y + 0.01f, 0);
		}
	}
}
