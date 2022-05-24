using Spine.Unity;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	internal AnimationController animController;

	private Entity entity; 

	private Level playerLevel;

	void Start()
	{
		animController = GetComponent<AnimationController>();
		entity = GetComponent<Entity>();
	}

	float timer = 0;
	void Update()
	{
		if (Input.GetKey(KeyCode.Tab) && timer > .5f)
		{
			ChangeAnimMode(timer);
		}
		timer += Time.deltaTime;
	}

	// Изменяет скин игрока.
	void ChangeAnimMode(float timer)
	{
		if (animController.skeletonAnimation.skeleton.Skin.ToString() == "swordsman")
			animController.skeletonAnimation.Skeleton.SetSkin("archer");
		else
			animController.skeletonAnimation.Skeleton.SetSkin("swordsman");

		this.timer = 0;
	}
}
