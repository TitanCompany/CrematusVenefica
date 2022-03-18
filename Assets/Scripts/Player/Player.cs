using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
	SpriteRenderer spriteRender;
	Animator animator;
	PlayerCombat playerCombat;

	Transform plTransform;
	Transform transformAttackPoint;

	// Определяет направление (>1 - право, <1 - лево).
	float _previousDirection;

	void Start()
	{
		spriteRender = GetComponent<SpriteRenderer>();
		playerCombat = GetComponent<PlayerCombat>();
		animator = GetComponent<Animator>();
		plTransform = GetComponent<Transform>();
		transformAttackPoint = playerCombat.attackPoint.transform;
	}

	void FixedUpdate()
	{
		float currentDirection;
		float hAxis = Input.GetAxis("Horizontal");
		if (hAxis > 0)
			currentDirection = 1f;
		else if (hAxis == 0)
			currentDirection = 0f;
		else
			currentDirection = -1f;
		print(currentDirection);

		if (currentDirection != _previousDirection && hAxis != 0)
		{
			_previousDirection = currentDirection;
			if (currentDirection < 0)
			{
				animator.SetTrigger("Run");
				ChangeDirection(true);
			}
			else if (currentDirection > 0)
			{
				animator.SetTrigger("Run");
				ChangeDirection(false);
			}
			else
			{

			}
		}
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

	private IEnumerator WaitAnim(float time)
	{
		yield return new WaitForSeconds(time);
	}
}
