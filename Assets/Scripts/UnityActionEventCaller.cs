using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityActionEventCaller : MonoBehaviour
{
    [SerializeField] private UnityEngine.Events.UnityEvent onCallEvent = new UnityEngine.Events.UnityEvent();

    public void InvokeUnityAction()
    {
        onCallEvent?.Invoke();
    }
}
