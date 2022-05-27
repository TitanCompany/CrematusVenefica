using System;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
	public float speed;
	public GameObject sound;

	private Vector2 movement;
	private Rigidbody2D body;
	private AnimationController animController;

	void Start()
	{
		body = GetComponent<Rigidbody2D>();
		animController = GetComponent<AnimationController>();
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
			if (animController.currentAnimation != "dash" && animController.currentAnimation != "sword_strike")
			{
				animController.SetCharacterState("run", 1f, true);
				Instantiate(sound, transform.position, Quaternion.identity);
				//Звук бега
			}

			if (movement.x > 0)
				transform.localScale = new Vector2(Math.Abs(transform.localScale.x), transform.localScale.y);
			else if (movement.x < 0)
				transform.localScale = new Vector2(-Math.Abs(transform.localScale.x), transform.localScale.y);
			
			body.MovePosition(body.position + movement * speed * Time.fixedDeltaTime);
		}
		else if (animController.currentState != "dash" && animController.currentState != "sword_strike" && animController.currentState != "idle")
			animController.SetCharacterState("idle", 1, true);//не надо звуков
	}
}
