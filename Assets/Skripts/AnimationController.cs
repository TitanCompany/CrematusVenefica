using Spine.Unity;
using UnityEngine;

public class AnimationController: MonoBehaviour
{
	public Animation[] anims;
	public GameObject obj;
	public Animation standartAnim;

	private SkeletonAnimation skeletonAnimation;
	private string currentAnimation;
	private string previousState;
	private string currentState;

	private string skin;

	/// <summary>
	/// Конструктор Контроллера анимаций.
	/// Стандартная анимация задана по умолчанию (idle, true, 1f).
	/// </summary>
	/// <param name="anims">Массив всех возможных анимаций, за исключением стандартной.</param>
	/// <param name="obj">Объект, на который будет примерятся анимация.</param>
	public AnimationController(Animation[] anims, GameObject obj)
	{
		this.anims = anims;
		this.obj = obj;
		var idle = new AnimationReferenceAsset();
		standartAnim = new Animation(idle, true, 1f);
	}

	/// <summary>
	/// Конструктор Контроллера анимаций.
	/// </summary>
	/// <param name="anims">Массив всех возможных анимаций, за исключением стандартной.</param>
	/// <param name="obj">Объект, на который будет примерятся анимация.</param>
	/// <param name="standartAnim">Устанавливает стандартную анмацию.</param>
	public AnimationController(Animation[] anims, GameObject obj, Animation standartAnim)
	{
		this.anims = anims;
		this.obj = obj;
		this.standartAnim = standartAnim;
	}

	/// <summary>
	/// Задать анимацию объекту.
	/// </summary>
	/// <param name="state">Название анимации.</param>
	public void SetCharacterState(string state)
	{
		foreach (var anim in anims)
			if (anim.asset.name == state)
			{
				SetAnimation(anim);
				return;
			}

		SetAnimation(standartAnim);
	}

	public void SetAnimation(Animation anim)
	{
		if (anim.asset.name == currentAnimation)
			return;
		Spine.TrackEntry animationEntry = skeletonAnimation.state.SetAnimation(0, anim.asset, anim.isLoop);
		animationEntry.TimeScale = anim.timeScale;
		//animationEntry.Complete += AnimationEntryComplete;
		currentAnimation = anim.asset.name;
	}

	/// <summary>
	/// Обозначает класс конфигурации анимации.
	/// </summary>
	public class Animation
	{
		internal AnimationReferenceAsset asset;
		internal bool isLoop;
		internal float timeScale;

		public Animation(AnimationReferenceAsset asset, bool isLoop, float timeScale)
		{
			this.asset = asset;
			this.isLoop = isLoop;
			this.timeScale = timeScale;
		}
	}
}
