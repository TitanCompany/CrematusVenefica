﻿using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
	public abstract int MaxHP { get; set; }
	public abstract int CurrentHP { get; set; }
	public abstract bool IsDie { get; set; }

	public abstract void TakeDamage(int damage);

	public abstract void Die();
}
