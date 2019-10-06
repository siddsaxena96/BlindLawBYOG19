using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Events;

public class UIController : MonoBehaviour, IEventListener
{

    public ConversationController conversationController;
    [SerializeField] private Core.Events.EventWithParameteres OnConversationEvent = null;

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

    // Update is called once per frame
    void Update()
    {
        
    }
}
