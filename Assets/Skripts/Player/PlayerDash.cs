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
	public float dashDuration;
	public float dashTimer;

	private DashDirection dashDirection;


	private PlayerController plController;
	private Rigidbody2D body;
	private Vector2 movement;

	private float x;
	private float y;

	void Start()
	{
		plController = GetComponent<PlayerController>();
		body = GetComponent<Rigidbody2D>();
		dashDirection = DashDirection.NoDirection;
	}

	void Update()
	{
		body.velocity = Vector2.zero;
		x = Input.GetAxis("Horizontal");
		y = Input.GetAxis("Vertical");
		if ((x != 0 || y != 0) && Input.GetKey(KeyCode.LeftShift))
		{
			KeyCheck();
			Dash();
		}
	}
	void KeyCheck()
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
			if (dashTimer >= dashDuration)
			{
				dashTimer = 0;
				dashDirection = DashDirection.NoDirection;
				body.velocity = Vector2.zero;
			}
			else
			{
				dashTimer += Time.deltaTime;
				switch (dashDirection)
				{
					case DashDirection.Left:
						body.velocity = Vector2.left * dashSpeed;
						plController.animController.SetCharacterState("dash");
						break;
					case DashDirection.Right:
						body.velocity = Vector2.right * dashSpeed;
						plController.animController.SetCharacterState("dash");
						break;
					case DashDirection.Up:
						body.velocity = Vector2.up * dashSpeed;
						plController.animController.SetCharacterState("dash");
						break;
					case DashDirection.Down:
						body.velocity = Vector2.down * dashSpeed;
						plController.animController.SetCharacterState("dash");
						break;
					case DashDirection.LeftUp:
						body.velocity = Vector2.left * dashSpeed + Vector2.up * dashSpeed;
						plController.animController.SetCharacterState("dash");
						break;
					case DashDirection.LeftDown:
						body.velocity = Vector2.left * dashSpeed + Vector2.down * dashSpeed;
						plController.animController.SetCharacterState("dash");
						break;
					case DashDirection.RightUp:
						body.velocity = Vector2.right * dashSpeed + Vector2.up * dashSpeed;
						plController.animController.SetCharacterState("dash");
						break;
					case DashDirection.RightDown:
						body.velocity = Vector2.right * dashSpeed + Vector2.down * dashSpeed;
						plController.animController.SetCharacterState("dash");
						break;
					default: break;
				}
			}
		}
	}
}
