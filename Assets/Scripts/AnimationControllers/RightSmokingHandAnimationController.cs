using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RightSmokingHandAnimationController : AnimationHandler
{
    [SerializeField] private Animator cigaretteAnimator = null;

    public override void OnReceiveEvent(int eventInput)
    {
        if(eventInput == 0){
            cigaretteAnimator.SetBool("DragOn",true);
        }

        else{
            cigaretteAnimator.SetBool("DragOn",false);
            Debug.Log("Here");
        }
    }
}
