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
		
	}

	

	private IEnumerator WaitAnim(float time)
	{
		yield return new WaitForSeconds(time);
	}
}
