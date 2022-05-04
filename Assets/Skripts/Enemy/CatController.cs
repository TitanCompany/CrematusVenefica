using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class CatController : MonoBehaviour
{
    public SkeletonAnimation skeletonAnimation;
    public AnimationReferenceAsset idle, run;
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

    public void SetCharacterState(string state)
    {

        if (state.Equals("Run"))
        {
            SetAnimation(run, true, 2f);
        }

        else
        {
            SetAnimation(idle, true, 1f);
        }

        currentState = state;
    }

    public void SetAnimation(AnimationReferenceAsset animation, bool loop, float timeScale)
    {
        if (animation.name.Equals(currentAnimation))
        {
            return;
        }
        Spine.TrackEntry animationEntry = skeletonAnimation.state.SetAnimation(0, animation, loop);
        animationEntry.TimeScale = timeScale;
        animationEntry.Complete += AnimationEntry_Complete;
        currentAnimation = animation.name;
    }

    private void AnimationEntry_Complete(Spine.TrackEntry trackEntry)
    {
        if (currentState.Equals("Dash"))
        {
            SetCharacterState(previousState);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
