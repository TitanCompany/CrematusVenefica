using UnityEngine;

public class PlayerDash : MonoBehaviour
{
	private enum DashDirection
	{
		Left,
		Right,
		Up,
		Down,
		LeftUp,
		LeftDown,
		RightUp,
		RightDown,
		NoDirection
	}

	public float dashSpeed;
	public float startDashTime;
	private float dashTime;

	private DashDirection dashDirection;

	private AnimationController animController;
	private Rigidbody2D body;

	private float x;
	private float y;

	void Start()
	{
		body = GetComponent<Rigidbody2D>();
		animController = GetComponent<AnimationController>();
		dashDirection = DashDirection.NoDirection;
		dashTime = startDashTime;
	}

	void Update()
	{
		body.velocity = Vector2.zero;
		x = Input.GetAxis("Horizontal");
		y = Input.GetAxis("Vertical");
		if ((x != 0 || y != 0) && Input.GetKey(KeyCode.LeftShift))
		{
			CheckDirection();
			Dash();
		}
	}

	void CheckDirection()
	{
		if (x > 0 && y > 0) dashDirection = DashDirection.RightUp;
		else if (x < 0 && y > 0) dashDirection = DashDirection.LeftUp;
		else if (x > 0 && y < 0) dashDirection = DashDirection.RightDown;
		else if (x < 0 && y < 0) dashDirection = DashDirection.LeftDown;

		else if (y > 0) dashDirection = DashDirection.Up;
		else if (y < 0) dashDirection = DashDirection.Down;
		else if (x < 0) dashDirection = DashDirection.Left;
		else if (x > 0) dashDirection = DashDirection.Right;

		else dashDirection = DashDirection.NoDirection;
	}

	void Dash()
	{
		if (dashDirection != DashDirection.NoDirection)
		{
			float time = 1f;

			if (dashTime <= 0)
			{
				dashTime = startDashTime;
				dashDirection = DashDirection.NoDirection;
				body.velocity = Vector2.zero;
			}
			else
			{
				dashTime -= Time.deltaTime;
				switch (dashDirection)
				{
					case DashDirection.Left:
						animController.SetCharacterState("dash", 2f);
						body.velocity = Vector2.left * dashSpeed * time;
						break;
					case DashDirection.Right:
						animController.SetCharacterState("dash", 2f);
						body.velocity = Vector2.right * dashSpeed * time;
						break;
					case DashDirection.Up:
						animController.SetCharacterState("dash", 2f);
						body.velocity = Vector2.up * dashSpeed * time;
						break;
					case DashDirection.Down:
						animController.SetCharacterState("dash", 2f);
						body.velocity = Vector2.down * dashSpeed * time;
						break;
					case DashDirection.LeftUp:
						animController.SetCharacterState("dash", 2f);
						body.velocity = (Vector2.left + Vector2.up) / 2 * dashSpeed * time;
						break;
					case DashDirection.LeftDown:
						animController.SetCharacterState("dash", 2f);
						body.velocity = (Vector2.left + Vector2.down) / 2 * dashSpeed * time;
						break;
					case DashDirection.RightUp:
						animController.SetCharacterState("dash", 2f);
						body.velocity = (Vector2.right + Vector2.up) / 2 * dashSpeed * time;
						break;
					case DashDirection.RightDown:
						animController.SetCharacterState("dash", 2f);
						body.velocity = (Vector2.right + Vector2.down) / 2 * dashSpeed * time;
						break;
					default: break;
				}
			}
		}
	}
}
