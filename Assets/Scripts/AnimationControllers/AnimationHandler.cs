using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AnimationHandler : MonoBehaviour
{
    public Animator animator = null;

    private void Awake()
    {
        if (animator == null)
            animator = GetComponent<Animator>();
    }

    public virtual void OnReceiveEvent(bool eventInput) { }
    public virtual void OnReceiveEvent(int eventInput) { }
    
}
