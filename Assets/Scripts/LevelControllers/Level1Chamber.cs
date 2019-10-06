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
    private bool dialogueStarted = false;
    [SerializeField] private Core.Events.Event OnConversationEnded = null;
    private int levelStepIndex = -1;
    public int levelStepCount = -1;

    private readonly string DialogueSequence1 = "U: The accused \n P: and you answered? ";

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


        StartDialogueSequence(DialogueSequence1);
         while(dialogueStarted == true)
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
    }

    private void StartDialogueSequence(string dialogueSequence)
    {
        dialogueStarted = true;
        uiController.NormalConversation(dialogueSequence);
        StartCoroutine(WaitForDialogueToEnd());
    }

    IEnumerator WaitForDialogueToEnd()
    {
        while(dialogueStarted == true)
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
}
