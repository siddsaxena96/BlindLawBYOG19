using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Core.Events;

public class UIController : MonoBehaviour, IEventListener
{

    public ConversationController conversationController;
    [SerializeField] private Core.Events.EventWithParameteres OnConversationEvent = null;
    [SerializeField] private Image fadePanel = null;
    [SerializeField] private Core.Events.Event fadeComplete = null;

    public void OnEventRaised()
    {
        throw new System.NotImplementedException();
    }

    public void OnEventRaisedWithParameters(List<object> parameters)
    {
        throw new System.NotImplementedException();

    }


    void Start()
    {

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

    public void FadeToBlack()
    {
        StartCoroutine(FadePanel(0));
    }

    public void FadeFromBlack()
    {
        StartCoroutine(FadePanel(1));
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
    // Update is called once per frame
    void Update()
    {

    }
}
