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
	/// ����������� ����������� ��������.
	/// ����������� �������� ������ �� ��������� (idle, true, 1f).
	/// </summary>
	/// <param name="anims">������ ���� ��������� ��������, �� ����������� �����������.</param>
	/// <param name="obj">������, �� ������� ����� ���������� ��������.</param>
	public AnimationController(Animation[] anims, GameObject obj)
	{
		this.anims = anims;
		this.obj = obj;
		var idle = new AnimationReferenceAsset();
		standartAnim = new Animation(idle, true, 1f);
	}

	/// <summary>
	/// ����������� ����������� ��������.
	/// </summary>
	/// <param name="anims">������ ���� ��������� ��������, �� ����������� �����������.</param>
	/// <param name="obj">������, �� ������� ����� ���������� ��������.</param>
	/// <param name="standartAnim">������������� ����������� �������.</param>
	public AnimationController(Animation[] anims, GameObject obj, Animation standartAnim)
	{
		this.anims = anims;
		this.obj = obj;
		this.standartAnim = standartAnim;
	}

	/// <summary>
	/// ������ �������� �������.
	/// </summary>
	/// <param name="state">�������� ��������.</param>
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
