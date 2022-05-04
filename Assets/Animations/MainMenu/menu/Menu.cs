using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class Menu : MonoBehaviour
{
    public SkeletonGraphic skeletonGraphic;

    public void NewGameHoverIn(){
        skeletonGraphic.timeScale = 1.0f;
        skeletonGraphic.AnimationState.SetAnimation(1, "newGameHoverIn", false);
    }

    public void NewGameHoverOut(){
        skeletonGraphic.AnimationState.SetAnimation(1, "newGameHoverOut", false);
    }

    public void ContinueHoverIn(){
        
    }

    public void ContinueHoverOut(){
        
    }    
    
    public void ExitHoverIn(){
        skeletonGraphic.timeScale = 1.0f;
        skeletonGraphic.AnimationState.SetAnimation(1, "exitHoverIn", false);
    }    
    
    public void ExitHoverOut(){
        skeletonGraphic.AnimationState.SetAnimation(1, "exitHoverOut", false);
    }
}
