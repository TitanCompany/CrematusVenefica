using Spine;
using Spine.Unity;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
	public SkeletonAnimation skeletonAnimation;
	public AnimationReferenceAsset idleAsset;
	public AnimationReferenceAsset[] assets;
	public string currentAnimation;
	public string previousState;
	public string currentState;
	public string[] spesialAnims;

	private string skin;

	private void Start()
	{
		if (spesialAnims == null)
			spesialAnims = new string[0];
	}

	/// <summary>
	/// Задать анимацию объекту.
	/// </summary>
	/// <param name="state">Название анимации.</param>
	public void SetCharacterState(string state, float time, bool isLoop = false)
	{
		var isFind = false;
		foreach (var asset in assets)
			if (asset.name == state)
			{
				SetAnimation(asset, time, isLoop);
				isFind = true;
				break;
			}

		if (!isFind) SetAnimation(idleAsset, 1f, true);
		previousState = currentState;
		currentState = state;
	}

	private float time;
	private bool isLoop;

	/// <summary>
	/// Устанавливает анимацию.
	/// </summary>
	/// <param name="asset"></param>
	private void SetAnimation(AnimationReferenceAsset asset, float time, bool isLoop)
	{
		if (asset.name == currentAnimation)
			return;
		TrackEntry animationEntry = skeletonAnimation.state.SetAnimation(0, asset, isLoop);
		//animationEntry.TrackTime = time;
		animationEntry.TimeScale = time;

		this.time = time;
		this.isLoop = isLoop;

		animationEntry.Complete += AnimationEntryComplete;
		currentAnimation = asset.name;
	}

	/// <summary>
	/// Изменяет анимацию, после ее завершения на предидущую.
	/// Если это необходимо.
	/// </summary>
	/// <param name="trackEntry"></param>
	private void AnimationEntryComplete(TrackEntry trackEntry)
	{
		foreach (var anim in spesialAnims)
			if (currentState == anim)
			{
				SetCharacterState("idle", time, isLoop);
			}
	}


	/*
	/// <summary>
	/// Создает базовый присет анимаций.
	/// </summary>
	/// <param name="skeleton">Задает скелет для Spine.Skeleton</param>
	/// <param name="standartAnim">Задает страндартную анимация (например: idle)</param>
	/// <param name="anims">Перечисление остальных анимаций</param>
	public AnimationController(SkeletonAnimation skeleton, Animation standartAnim, params Animation[] anims)
	{
		this.skeletonAnimation = skeleton;
		this.standartAnim = standartAnim;
		this.anims = anims;
	}

	/// <summary>
	/// Задать анимацию объекту.
	/// </summary>
	/// <param name="state">Название анимации.</param>
	public void SetCharacterState(string state)
	{
		var isFind = false;
		foreach (var anim in anims)
			if (anim.asset.name == state)
			{
				SetAnimation(anim);
				isFind = true;
				break;
			}

		if (!isFind) SetAnimation(standartAnim);
		previousState = currentState;
		currentState = state;
	}

	/// <summary>
	/// Устанавливает анимацию.
	/// </summary>
	/// <param name="anim"></param>
	private void SetAnimation(Animation anim)
	{
		if (anim.asset.name == currentAnimation)
			return;
		TrackEntry animationEntry = skeletonAnimation.state.SetAnimation(0, anim.asset, anim.isLoop);
		animationEntry.TrackTime = anim.timeScale;
		animationEntry.TimeScale = anim.timeScale;
		animationEntry.Complete += AnimationEntryComplete;
		currentAnimation = anim.asset.name;
	}

	/// <summary>
	/// Изменяет анимацию, после ее завершения на предидущую.
	/// Если это необходимо.
	/// </summary>
	/// <param name="trackEntry"></param>
	private void AnimationEntryComplete(TrackEntry trackEntry)
	{
		foreach (var anim in spesialAnims)
			if (currentState == anim)
			{
				SetCharacterState(previousState);
			}

		/*if (currentState.Equals("dash"))
		{
			SetCharacterState(previousState);
		}/
	}

	public void SetSpecialAnims(params string[] spesialAnims)
	{
		this.spesialAnims = spesialAnims;
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
	}*/
}
