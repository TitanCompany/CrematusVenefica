using UnityEngine;
using UnityEngine.AI;

public abstract class Enemy : MonoBehaviour
{
	private NavMeshAgent agent;

	public abstract int MaxHP { get; set; }
	public abstract int CurrentHP { get; set; }
	public abstract bool IsDie { get; set; }

	public abstract void TakeDamage(int damage);

	public abstract void Die();

	public void SearchPathToPlayer()
	{
		
	}
}
