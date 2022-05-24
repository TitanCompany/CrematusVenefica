using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
	public float maxHP;
	[SerializeField]
	public float currentHP;
	public float speed;
	public int expirience;
   
	internal AnimationController animCtrl;
	internal Transform form;
	internal Rigidbody2D body;

	// TODO: Temp - Переписать класс Контроллера Анимаций.
	private CatController cat;


	// Start is called before the first frame update
	void Start()
	{
		form = GetComponent<Transform>();
		body = GetComponent<Rigidbody2D>();
		animCtrl = GetComponent<AnimationController>();
		currentHP = maxHP;

		// Temp
		cat = GetComponent<CatController>();
	}

	// Update is called once per frame
	void Update()
	{
		
	}

	public void TakeDamage(float damage)
	{
		currentHP -= damage;
		if (currentHP <= 0)
			Death();

		// TODO: Temp
		print(base.gameObject.name);
		if (base.gameObject.name != "Player")
		{
			cat.SetCharacterState("Damage");
		}
	}

	private void Death()
	{
		GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerLevel>().AddExp(expirience);

		// TODO: Temp
		if (base.gameObject.name != "Player")
		{
			cat.SetCharacterState("Death");
			cat.enabled = false;
		}
		else
		{
			new SceneChange().NextLevel("SampleScene");
		}
		//animCtrl.SetCharacterState("death");
	}
}
