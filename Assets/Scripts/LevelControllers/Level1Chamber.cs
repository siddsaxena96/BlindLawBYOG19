using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1Chamber : MonoBehaviour, ILevelController
{
    [SerializeField] private AnimationHandler smokingHand = null;
    [SerializeField] private NPCController underStudyController = null;
    [SerializeField] private NPCController client1 = null;
    [SerializeField] private Transform tableStandingPoint = null;
    [SerializeField] private Transform wallStandingPoint = null;
    [SerializeField] private Transform chairLeft = null;
    [SerializeField] private Transform chairRight = null;

    private void Start()
    {
        StartLevel();
    }

    public void StartLevel()
    {
        StartCoroutine(LevelCoroutine());
    }

    IEnumerator LevelCoroutine()
    {
        yield return new WaitForSeconds(2);
        smokingHand.animator.SetBool("TakeDrag", true);
        underStudyController.OnWalkTo(tableStandingPoint);
        while (underStudyController.isWalking)
        {
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(3);
        Debug.Log("Second Walk");
        underStudyController.OnWalkTo(wallStandingPoint);
        client1.OnWalkTo(chairLeft);
        while (underStudyController.isWalking)
        {
            yield return new WaitForSeconds(0.1f);
        }
        Debug.Log("Flip Now");
        underStudyController.FlipSpriteX();
        while (client1.isWalking)
        {
            yield return new WaitForSeconds(0.1f);
        }
        client1.OnSit();
    }
}
