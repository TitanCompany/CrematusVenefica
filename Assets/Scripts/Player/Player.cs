using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
	SpriteRenderer spriteRender;
	Animator animator;
	PlayerCombat playerCombat;

	

	public float MaxHealthPoints;
	public float currentHealthPoints;

	void Start()
	{
		spriteRender = GetComponent<SpriteRenderer>();
		playerCombat = GetComponent<PlayerCombat>();
		animator = GetComponent<Animator>();

		MaxHealthPoints = 100;
		currentHealthPoints = MaxHealthPoints;
	}

	void FixedUpdate()
	{
		
	}

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
		// YouDeadText = "YOU DEAD";
		print("YOU DEAD");
	}
}
