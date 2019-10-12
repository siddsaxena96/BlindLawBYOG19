using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Core.Events;

public class UIController : MonoBehaviour
{

    public ConversationController conversationController;
    [SerializeField] private Core.Events.EventWithParameteres OnConversationEvent = null;
    [SerializeField] private Image fadePanel = null;
    [SerializeField] private Image highlightPanel = null;
    [SerializeField] private Core.Events.Event fadeComplete = null;
    [SerializeField] private Core.Events.Event highlightComplete = null;
    [SerializeField] private RectTransform pointer = null;
    [SerializeField] private Vector2 pointerOffset = Vector2.zero;
    private bool objectHighlighted = false;

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

    public void FadeToBlack()
    {
        StartCoroutine(FadePanel(0));
    }

    public void FadeFromBlack()
    {
        StartCoroutine(FadePanel(1));
    }

    public void HighlighObject(GameObject objectToHighlight)
    {
        StartCoroutine(HighlighPanel(objectToHighlight));
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

    IEnumerator HighlighPanel(GameObject toHighlight)
    {
        int oldOrder = toHighlight.GetComponentInChildren<SpriteRenderer>().sortingOrder;
        Transform currentParent = toHighlight.transform.parent;
        toHighlight.transform.SetParent(transform);
        toHighlight.transform.SetAsLastSibling();
        foreach (SpriteRenderer item in toHighlight.GetComponentsInChildren<SpriteRenderer>())
        {
            item.sortingOrder = GetComponent<Canvas>().sortingOrder + 1;
        }

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
