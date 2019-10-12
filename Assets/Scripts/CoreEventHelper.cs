using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Events;

public class CoreEventHelper : MonoBehaviour, IEventListener
{
    [SerializeField] private Core.Events.Event eventToRaise = null;
    [SerializeField] private UnityEngine.Events.UnityEvent onEventRaise = new UnityEngine.Events.UnityEvent();


    private void Awake()
    {
        eventToRaise.RegisterListener(this);
    }

    public void OnEventRaised()
    {
        onEventRaise?.Invoke();
    }

    public void OnEventRaisedWithParameters(List<object> parameters)
    {
        throw new System.NotImplementedException();
    }

    private void OnDestroy()
    {
        eventToRaise.UnregisterListener(this);
    }
}
