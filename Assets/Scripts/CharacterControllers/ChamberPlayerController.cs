using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChamberPlayerController : MonoBehaviour
{
    [SerializeField] private AnimationHandler playerSmokingHand = null;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            playerSmokingHand?.AttemptAnimation("TakeDrag", true);
        }
    }
}
