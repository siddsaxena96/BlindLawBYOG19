using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Events;
using UnityEngine.EventSystems;

public class ConversationHelper : MonoBehaviour, IEventListener, IPointerClickHandler
{
    public Core.Events.Event OnConversationClicked = null;
    public Core.Events.Event OnClickListen = null;
    private bool clickStart = false;
    public void OnEventRaised()
    {
        clickStart = true;
    }

    public void OnEventRaisedWithParameters(List<object> parameters)
    {
        throw new System.NotImplementedException();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(clickStart == true)
        {
            OnConversationClicked?.Raise();
            clickStart = false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        OnClickListen?.RegisterListener(this);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
