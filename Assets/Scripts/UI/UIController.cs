using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Core.Events;

public class UIController : MonoBehaviour
{

    [Header("Conversation System")]
    public ConversationController conversationController;
    [SerializeField] private Core.Events.EventWithParameteres OnConversationEvent = null;
    
    [Header("Fade Panel")]
    [SerializeField] private Image fadePanel = null;
    [SerializeField] private Core.Events.Event fadeComplete = null;

    [Header("Highlight Panel")]
    [SerializeField] private GameObject guidanceSystem = null;
    [SerializeField] private Image highlightPanel = null;    
    [SerializeField] private Core.Events.Event highlightComplete = null;
    [SerializeField] private RectTransform pointer = null;
    [SerializeField] private Vector2 pointerOffset = Vector2.zero;
    [SerializeField] private Text guidanceText = null;
    private bool objectHighlighted = false;


    [Header("CourtEvents")]
    [SerializeField] private CourtEvent courtEvent = null;
    [SerializeField] private Core.Events.EventWithParameteres OnObjectionRaised, OnSherlock;

    public void ObjectionEvent(int min, int max, float t)
    {
        List<object> o = new List<object>();
        object a = CourtEvent.CourtEvents.Objection;
        object b = (int)min;
        object c = (int)max;
        object timer = t;
        o.Clear();
        o.Add(a);
        o.Add(b);
        o.Add(c);
        o.Add(timer);
        OnObjectionRaised.Raise(o);
    }

    public void Sherlock(string[] s, float alphaLag, float sentLag)
    {
        object a = CourtEvent.CourtEvents.Sherlock;
        object b = s;
        object c = alphaLag;
        object d = sentLag;
        List<object> o = new List<object>();
        o.Add(a);
        o.Add(b);
        o.Add(c);
        o.Add(d);
        OnSherlock.Raise(o);

    }

    public void NormalConversation(string dialogue)
    {
        List<object> temp = new List<object>();
        temp.Add(dialogue);
        OnConversationEvent?.Raise(temp);
    }

    public void StartConversation(string[] dialogues)
    {
        List<object> temp = new List<object>();
        temp.Add(dialogues);

        OnConversationEvent?.Raise(temp);
    }

    public void StartConversationWithColor( Dialouge[] dialouges)
    {
        conversationController.ConversationWithColor(dialouges);
    }

    public void FadeToBlack()
    {
        StartCoroutine(FadePanel(0));
    }

    public void FadeFromBlack()
    {
        StartCoroutine(FadePanel(1));
    }

    public void HighlighObject(GameObject objectToHighlight,string guidanceString)
    {
        StartCoroutine(HighlighPanel(objectToHighlight,guidanceString));
    }

    public void ToggleFadePanel(bool status)
    {
        fadePanel.gameObject.SetActive(status);
    }

    IEnumerator FadePanel(int fadeFrom)
    {
        switch (fadeFrom)
        {
            case 0:
                for (float ft = 0f; ft <= 1; ft += 0.1f)
                {
                    Color c = fadePanel.color;
                    c.a = ft;
                    fadePanel.color = c;
                    yield return new WaitForSeconds(.1f);
                }
                break;
            case 1:
                for (float ft = 1f; ft >= 0; ft -= 0.1f)
                {
                    Color c = fadePanel.color;
                    c.a = ft;
                    fadePanel.color = c;
                    yield return new WaitForSeconds(.1f);
                }
                break;
        }
        fadeComplete?.Raise();
        yield return new WaitForSeconds(0.1f);
    }

    IEnumerator HighlighPanel(GameObject toHighlight,string guidanceString)
    {        
        int oldOrder = toHighlight.GetComponentInChildren<SpriteRenderer>().sortingOrder;
        Transform currentParent = toHighlight.transform.parent;
        toHighlight.transform.SetParent(transform);
        toHighlight.transform.SetAsLastSibling();
        foreach (SpriteRenderer item in toHighlight.GetComponentsInChildren<SpriteRenderer>())
        {
            item.sortingOrder = GetComponent<Canvas>().sortingOrder + 1;
        }
        guidanceText.text = guidanceString;
        guidanceSystem.SetActive(true);
        for (float ft = 0f; ft <= 0.8; ft += 0.2f)
        {
            Color c = highlightPanel.color;
            c.a = ft;
            highlightPanel.color = c;
            yield return new WaitForSeconds(.1f);
        }

        pointer.position = new Vector2(toHighlight.transform.position.x, toHighlight.transform.position.y) + pointerOffset;
        pointer.SetAsLastSibling();
        pointer.gameObject.SetActive(true);
        objectHighlighted = true;

        while (objectHighlighted)
            yield return null;

        foreach (SpriteRenderer item in toHighlight.GetComponentsInChildren<SpriteRenderer>())
        {
            item.sortingOrder = oldOrder;
        }
        toHighlight.transform.SetParent(currentParent);
        pointer.gameObject.SetActive(false);
        guidanceText.text = "";
        for (float ft = 0.8f; ft >= 0; ft -= 0.2f)
        {
            Color c = highlightPanel.color;
            c.a = ft;
            highlightPanel.color = c;
            yield return new WaitForSeconds(.1f);
        }
        Debug.Log("Raising Highlight Complete");
        highlightComplete?.Raise();
        yield return new WaitForSeconds(0.1f);
    }

    public void OnItemClicked()
    {
        if (objectHighlighted)
            objectHighlighted = false;
    }
}
