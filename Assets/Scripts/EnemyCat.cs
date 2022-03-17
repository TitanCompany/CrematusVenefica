using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCat : Enemy
{
	public override int MaxHP { get; set; }
	public override int currentHP { get; set; }

	void Start()
    {
		MaxHP = 100;
        currentHP = MaxHP;
    }

	public override void TakeDamage(int damage)
	{
		currentHP -= damage;

		if (currentHP <= 0)
		{
			Die();
		}
	}

	public override void Die()
	{
		Debug.Log("Enemy is Dead");
		// TODO: Die Animation 
		// TODO: Enemy is disable
	}
}
