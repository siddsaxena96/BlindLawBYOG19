using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1Chamber : MonoBehaviour,ILevelController
{
    [SerializeField] private AnimationHandler smokingHand = null;
    [SerializeField] private UnderStudyController underStudyController = null;

    private void Start() {
        StartLevel();
    }

    public void StartLevel()
    {
        StartCoroutine(LevelCoroutine());
    }

    IEnumerator LevelCoroutine()
    {
        yield return new WaitForSeconds(2);
        smokingHand.animator.SetBool("TakeDrag",true);
        underStudyController.WalkToChamberStandingPoint();
    }
}
