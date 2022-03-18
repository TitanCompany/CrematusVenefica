using UnityEngine;

public class EnemyCat : Enemy
{
	public override int MaxHP { get; set; }
	public override int CurrentHP { get; set; }
	public override bool IsDie { get; set; }

	Transform transform;

	void Start()
	{
		transform = GetComponent<Transform>();
		MaxHP = 100;
		CurrentHP = MaxHP;
		IsDie = false;
	}

	public override void TakeDamage(int damage)
	{
		if (!IsDie)
		{
			CurrentHP -= damage;

			if (CurrentHP <= 0)
				Die();
		}
	}

	public override void Die()
	{
		IsDie = true;
		Debug.Log("Enemy is Dead");
		// TODO: Die Animation 
		// animator.SetBool("IsDead", true);
		transform.Rotate(0f, 0f, 45f);
		// TODO: Enemy is disable
		GetComponent<Collider2D>().enabled = false;
		transform.position = new Vector3(transform.position.x, transform.position.y, 1f);
		this.enabled = false;
	}
}
