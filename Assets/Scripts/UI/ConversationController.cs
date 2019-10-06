using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Events;
using UnityEngine.UI;
using System;
using System.Text.RegularExpressions;

public class ConversationController : VuforiaMonoBehaviour, IEventListener
{

    [SerializeField] private Text text = null;
    [SerializeField] private Animator anim = null;
    private string[] dialogues = null;
    public string[] Dialogues => dialogues;
    private int dialogueIndex = -1;
    private int dialogueCount => dialogues.Length;
    private bool hasNextDialogue => dialogueIndex < dialogues.Length - 1;
    [SerializeField] private Button interactButton = null;
    private Coroutine coroutine = null;

    private readonly string convoString = "convoString";
    private bool clicked = false;
    [SerializeField] private Core.Events.EventWithParameteres OnConversationEvent = null;


    void Start()
    {
        if (anim == null)
        {
            anim = this.GetComponent<Animator>();
        }
        OnConversationEvent?.RegisterListener(this);
    }

    public void NormalConversation(string s)
    {
        text.text = s;
        ResetAnimation();
        StartAnimation();
    }

    private void StartAnimation()
    {
        anim.SetBool(convoString, true);
    }

    private void StopAnimation()
    {
        anim.SetBool(convoString, false);
    }

    private void ResetAnimation()
    {
        anim.SetBool(convoString, false);
    }

    public void OnEventRaised()
    {
        throw new System.NotImplementedException();
    }

    public void OnEventRaisedWithParameters(List<object> parameters)
    {
        foreach (object param in parameters)
        {
            var text = (string)param as string;
            dialogues = text.Split(new Char[] { '?', '!', ',', '.', ':', '\t', '\n' });
            //NormalConversation(text);
            TryConversation();
        }
    }

    void TryConversation()
    {
        if(hasNextDialogue)
        {
            NextConversation();
        }
        else
        {
        anim.SetBool(convoString, false);

            Debug.Log("Conversation Ended! ");
        }
    }

    void NextConversation()
    {
            this.text.text = dialogues[GetNextDialogueIndex()];
            Conversation(true);
            StartCoroutine(WaitForClickInput());
        
        
    }

    int GetNextDialogueIndex()
    {
        dialogueIndex++;
        dialogueIndex = dialogueIndex % dialogueCount;
        return dialogueIndex;
    }

    public void Conversation(bool isHappening)
    {
        anim.SetBool(convoString, true);
        interactButton.gameObject.SetActive(true);

    }

    public void Clicked()
    {
        clicked = true;
    }


    IEnumerator WaitForClickInput()
    {
        interactButton.onClick.AddListener(Clicked);
        while (clicked == false)
        {
            yield return null;
        }
        clicked = false;
        Conversation(false);
        interactButton.onClick.RemoveAllListeners();
        TryConversation();
    }
}