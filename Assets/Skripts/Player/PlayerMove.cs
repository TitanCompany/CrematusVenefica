using System;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
	public float speed;

	private Vector2 movement;
	private Rigidbody2D body;
	private PlayerController plController;

	void Start()
	{
		body = GetComponent<Rigidbody2D>();
		plController = GetComponent<PlayerController>();
	}

	void Update()
	{
		Move();
	}

	internal void Move()
	{
		movement.x = Input.GetAxis("Horizontal");
		movement.y = Input.GetAxis("Vertical");

		if (movement.x != 0 || movement.y != 0)
		{
			if (plController.animController.currentState != "dash")
				plController.animController.SetCharacterState("run");

			if (movement.x > 0)
				transform.localScale = new Vector2(Math.Abs(transform.localScale.x), transform.localScale.y);
			else if (movement.x < 0)
				transform.localScale = new Vector2(-Math.Abs(transform.localScale.x), transform.localScale.y);
		}
		else
			if (plController.animController.currentState != "dash")
				plController.animController.SetCharacterState("idle");

		body.MovePosition(body.position + movement * speed * Time.fixedDeltaTime);
	}
}
