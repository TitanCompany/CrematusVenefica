using Spine;
using Spine.Unity;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
	//public SkeletonAnimation skeleton;
	//[SerializeField]
	//public (AnimationReferenceAsset, float, bool)[] assets;
	//public AnimationReferenceAsset idleAsset;

	public Animation[] anims;
	public Animation standartAnim;
	internal string[] spesialAnims;

	internal SkeletonAnimation skeletonAnimation;
	public string currentAnimation;
	public string previousState;
	public string currentState;

	private string skin;

	/// <summary>
	/// ������� ������� ������ ��������.
	/// </summary>
	/// <param name="skeleton">������ ������ ��� Spine.Skeleton</param>
	/// <param name="standartAnim">������ ������������ �������� (��������: idle)</param>
	/// <param name="anims">������������ ��������� ��������</param>
	public AnimationController(SkeletonAnimation skeleton, Animation standartAnim, params Animation[] anims)
	{
		this.skeletonAnimation = skeleton;
		this.standartAnim = standartAnim;
		this.anims = anims;
	}

	/// <summary>
	/// ������ �������� �������.
	/// </summary>
	/// <param name="state">�������� ��������.</param>
	public void SetCharacterState(string state)
	{
		var isFind = false;
		foreach (var anim in anims)
			if (anim.asset.name == state)
			{
				SetAnimation(anim);
				isFind = true;
				return;
			}

		if (!isFind) SetAnimation(standartAnim);
		previousState = currentState;
		currentState = state;
	}

	/// <summary>
	/// ������������� ��������.
	/// </summary>
	/// <param name="anim"></param>
	private void SetAnimation(Animation anim)
	{
		if (anim.asset.name == currentAnimation)
			return;
		TrackEntry animationEntry = skeletonAnimation.state.SetAnimation(0, anim.asset, anim.isLoop);
		animationEntry.TimeScale = anim.timeScale;
		animationEntry.Complete += AnimationEntryComplete;
		currentAnimation = anim.asset.name;
	}

	/// <summary>
	/// �������� ��������, ����� �� ���������� �� ����������.
	/// ���� ��� ����������.
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
		}*/
	}

	public void SetSpecialAnims(params string[] spesialAnims)
	{
		this.spesialAnims = spesialAnims;
	}

	/// <summary>
	/// ���������� ����� ������������ ��������.
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
