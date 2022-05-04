using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class PlayerController: MonoBehaviour
{
    public SkeletonAnimation skeletonAnimation;
    public AnimationReferenceAsset idle, walking, dash; 
    public string currentState;
    public float speed, dashSpeed;
    private Rigidbody2D rigidbody;
    public string currentAnimation;
    private Vector3 characterScale;
    Vector2 movement;
	public string previousState;

    // Start is called before the first frame update
    void Start()
    {
		rigidbody = GetComponent<Rigidbody2D>();
		characterScale = transform.localScale; 
		currentState = "Idle";
		SetCharacterState(currentState);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    // Set Character animation
    public void SetAnimation(AnimationReferenceAsset animation, bool loop, float timeScale)
    {
		if(animation.name.Equals(currentAnimation))
		{
			return;
		}
		Spine.TrackEntry animationEntry = skeletonAnimation.state.SetAnimation(0, animation, loop);
		animationEntry.TimeScale = timeScale;
		animationEntry.Complete += AnimationEntry_Complete;
		currentAnimation = animation.name;
    }

	// Do something after animation completes
	private void AnimationEntry_Complete(Spine.TrackEntry trackEntry)
    {
        if (currentState.Equals("Dash"))
        {
			SetCharacterState(previousState);
        }
    }

    // Check character state and sets the animation accordingly
    public void SetCharacterState(string state)
    {

		if(state.Equals("Walking"))
		{
			SetAnimation(walking, true, 2f);
		}
		else if (state.Equals("Dash"))
        {
			SetAnimation(dash, false, 2f);
        }

		else 
		{
			SetAnimation(idle, true, 1f);
		}

		currentState = state;
	}

    public void Move()
    {
		movement.x = Input.GetAxisRaw("Horizontal");
		movement.y = Input.GetAxisRaw("Vertical");

		rigidbody.MovePosition(rigidbody.position + movement * speed * Time.fixedDeltaTime);

		if(movement.x != 0 || movement.y != 0)
		{
            if (!currentState.Equals("Dash"))
            {
				SetCharacterState("Walking");
            }
			
			if(movement.x > 0)
			{
				transform.localScale = new Vector2(characterScale.x, characterScale.y);
			}
			else
			{
				transform.localScale = new Vector2(-characterScale.x, characterScale.y);
			}
		}
		else
		{
			if (!currentState.Equals("Dash"))
            {
				SetCharacterState("Idle");
            }
				
		}

		if (Input.GetButtonDown("Jump")) Dash();


		// Skin change example
		if (Input.GetButtonDown("Fire3"))
		{
			if(skeletonAnimation.skeleton.Skin.ToString() == "swordsman")
				skeletonAnimation.Skeleton.SetSkin("archer");
			else
				skeletonAnimation.Skeleton.SetSkin("swordsman");
			skeletonAnimation.Skeleton.SetSlotsToSetupPose();
		}

	}

	public void Dash()
    {
		rigidbody.velocity =  new Vector2(rigidbody.velocity.x, dashSpeed);
        if (!currentState.Equals("Dash"))
        {
			previousState = currentState;
        }
		SetCharacterState("Dash");
    }

}
