﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NPCController : MonoBehaviour
{
    [SerializeField] private AnimationHandler animationHandler = null;
    [SerializeField] private SpriteRenderer spriteRenderer = null;
    [SerializeField] private float movementSpeed = 5f;
    public bool isWalking = false;

    public void FlipSpriteX()
    {
        spriteRenderer.flipX = !spriteRenderer.flipX;
    }

    public void FlipSpriteY()
    {
        spriteRenderer.flipY = !spriteRenderer.flipY;
    }

    public void OnWalkTo(Transform destination)
    {
        StartCoroutine(WalkingCoroutine(destination.position,"WalkOn"));
    }

    public void OnSit()
    {
        animationHandler.animator.SetBool("SitOn", true);
    }

    public void OnStand()
    {
        animationHandler.animator.SetBool("SitOn", false);
    }

    public void OnRunTo(Transform destination)
    {
        StartCoroutine(WalkingCoroutine(destination.position,"RunOn"));
    }

    public void StopRun()
    {
        animationHandler.animator.SetBool("RunOn", false);
    }

    IEnumerator WalkingCoroutine(Vector3 destination,string boolName)
    {
        isWalking = true;
        animationHandler.animator.SetBool(boolName, true);
        while (Vector2.Distance(transform.position, destination) > 0.5f)
        {
            transform.position = Vector2.Lerp(transform.position, destination, movementSpeed);
            yield return new WaitForSeconds(0.1f);
        }
        isWalking = false;
        animationHandler.animator.SetBool(boolName, false);
    }
}
