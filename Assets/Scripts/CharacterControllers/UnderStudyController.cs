using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UnderStudyController : MonoBehaviour
{
    [SerializeField] private AnimationHandler animationHandler = null;
    [SerializeField] private float movementSpeed = 5f;

    [Header("Scene 1 : Chamber")]
    [SerializeField] private Transform chamberStandingPoint = null;

    private void Start()
    {
        WalkToChamberStandingPoint();
    }

    public void WalkToChamberStandingPoint()
    {
        animationHandler.SetBool("WalkOn", true);
        OnWalkTo(chamberStandingPoint);
    }

    public void OnWalkTo(Transform destination)
    {
        StartCoroutine(WalkingCoroutine(destination.position));
    }

    IEnumerator WalkingCoroutine(Vector3 destination)
    {
        while (Vector2.Distance(transform.position, destination) > 0.5f)
        {
            transform.position = Vector2.Lerp(transform.position, destination, movementSpeed);
            yield return new WaitForSeconds(0.1f);
        }
        animationHandler.SetBool("WalkOn", false);
    }
}
