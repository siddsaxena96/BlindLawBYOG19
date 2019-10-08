using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Events;

public class Level1Chamber : MonoBehaviour, ILevelController, IEventListener
{
    [SerializeField] private AnimationHandler smokingHand = null;
    [SerializeField] private NPCController underStudyController = null;
    [SerializeField] private NPCController client1 = null;
    [SerializeField] private Transform tableStandingPoint = null;
    [SerializeField] private Transform wallStandingPoint = null;
    [SerializeField] private Transform chairLeft = null;
    [SerializeField] private Transform chairRight = null;
    [SerializeField] private UIController uiController;
    [SerializeField] private DialougeAsset[] dialouges = null;
    private int dialougeNumber = 0;
    private bool dialogueStarted = false;
    [SerializeField] private Core.Events.Event OnConversationEnded = null;

    private void Start()
    {
        StartLevel();
        OnConversationEnded?.RegisterListener(this);
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

        //yield return new WaitForSeconds(3);


        StartNextDialogueSequence();
        while (dialogueStarted == true)
            yield return null;


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
        yield return new WaitForSeconds(2);
        StartNextDialogueSequence();
    }

    private void StartNextDialogueSequence()
    {
        dialogueStarted = true;
        uiController.ArrayConversation(dialouges[dialougeNumber].dialouges);
        dialougeNumber++;
        StartCoroutine(WaitForDialogueToEnd());
    }


    IEnumerator WaitForDialogueToEnd()
    {
        while (dialogueStarted == true)
        {
            yield return null;
        }

    }

    public void OnEventRaised()
    {
        dialogueStarted = false;
    }

    public void OnEventRaisedWithParameters(List<object> parameters)
    {
        throw new NotImplementedException();
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            smokingHand?.AttemptAnimation("TakeDrag", true);
        }
    }
}
