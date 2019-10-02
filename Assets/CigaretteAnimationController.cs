using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CigaretteAnimationController : AnimationHandler
{
    private void Update()
    {
        if (animator.GetBool("DragOn"))
            StartCoroutine(TakingDrag());
    }

    IEnumerator TakingDrag()
    {
        while (animator.GetBool("DragOn") == true)
            yield return new WaitForSeconds(0.1f);
        animator.enabled = false;

    }


}
