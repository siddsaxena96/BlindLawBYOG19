using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Events;
using UnityEngine.UI;
using System;
using System.Text.RegularExpressions;

public class ConversationController : MonoBehaviour, IEventListener
{

    [SerializeField] private Text resizeText = null;
    [SerializeField] private Text displayText = null;
    [SerializeField] private Animator anim = null;
    private string[] dialogues = null;
    public string[] Dialogues => dialogues;
    private int dialogueIndex = -1;
    private int dialogueCount => dialogues.Length;
    private bool hasNextDialogue => dialogueIndex < dialogues.Length - 1;
    [SerializeField] private Transform mainGameObject = null;
    [SerializeField] private Transform interactPanel = null;
    private Coroutine coroutine = null;

    private readonly string convoString = "convoString";
    private bool clicked = false;
    [SerializeField] private Core.Events.EventWithParameteres OnConversationEvent = null;
    [SerializeField] private Core.Events.Event OnConversationStarted = null, OnConversationEnded = null;
    private int dialogueIndexC = -1;
    private int dialogueCountC = -1;
    private bool hasNextDialogueC => dialogueIndexC < dialougesWithColor.Length - 1;
    private Dialouge[] dialougesWithColor = null;
    [SerializeField] private Core.Events.Event OnClicked = null;
    [SerializeField] private Core.Events.Event OnStartClick = null;

    void Start()
    {
        if (anim == null)
        {
            anim = this.GetComponent<Animator>();
        }
        OnConversationEvent?.RegisterListener(this);
        OnClicked?.RegisterListener(this);
    }

    private void OnDestroy()
    {
        OnConversationEvent?.UnregisterListener(this);
    }

    public void NormalConversation(string s)
    {
        resizeText.text = s;
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
        Clicked();
    }

    public void OnEventRaisedWithParameters(List<object> parameters)
    {
        dialogueIndex = -1;
        if (parameters != null)
        {

            foreach (object param in parameters)
            {
                string[] text = (string[])param as string[];
                dialogues = text;
                //text.Trim();
                //dialogues = text.Split(new Char[] { '?', '!', ',', '.', '\n' });
                if (dialogueCount > 0)
                {
                    OnConversationStarted?.Raise();
                    TryConversation();

                }
            }
        }
        else
        {
            Debug.Log("no conversation passed");
        }
    }

    internal void ConversationWithColor(Dialouge[] dialouges)
    {
        dialogueIndexC = -1;
        if (dialouges != null)
        {
            dialougesWithColor = dialouges;
            dialogueCountC = dialougesWithColor.Length;
            if (dialogueCountC > 0)
            {
                OnConversationStarted?.Raise();
                TryConversationC();
            }
        }
    }

    private void TryConversationC()
    {
        if (hasNextDialogueC)
        {
            NextConversationC();
        }
        else
        {
            anim.SetBool(convoString, false);
            OnConversationEnded?.Raise();
            Debug.Log("Conversation Ended! ");
        }

    }

    private void NextConversationC()
    {
        int index = GetNextDialogueIndexC();        
        this.resizeText.text = "\n" + dialougesWithColor[index].dialouge + "\n";
        this.displayText.color = dialougesWithColor[index].dialougeColor;
        this.displayText.text = dialougesWithColor[index].dialouge;
        Conversation(true);
        StartCoroutine(WaitForClickInputC());

    }

    private int GetNextDialogueIndexC()
    {
        dialogueIndexC++;
        dialogueIndexC = dialogueIndexC % dialogueCountC;
        return dialogueIndexC;
    }

    void TryConversation()
    {
        if (hasNextDialogue)
        {
            NextConversation();
        }
        else
        {
            anim.SetBool(convoString, false);
            OnConversationEnded?.Raise();
            Debug.Log("Conversation Ended! ");
        }
    }

    void NextConversation()
    {
        this.resizeText.text = dialogues[GetNextDialogueIndex()];
        this.displayText.text = dialogues[GetNextDialogueIndex()];
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
        mainGameObject.gameObject.SetActive(true);
        OnStartClick?.Raise();
    }

    public void Clicked()
    {
        clicked = true;
    }


    IEnumerator WaitForClickInput()
    {
        //mainGameObject.onClick.AddListener(Clicked);
        while (clicked == false)
        {
            yield return null;
        }
        clicked = false;
        Conversation(false);
        //mainGameObject.onClick.RemoveAllListeners();
        TryConversation();
    }

    IEnumerator WaitForClickInputC()
    {
        //mainGameObject.onClick.AddListener(Clicked);
        while (clicked == false)
        {
            yield return null;
        }
        clicked = false;
        Conversation(false);
        //mainGameObject.onClick.RemoveAllListeners();
        TryConversationC();
    }
}