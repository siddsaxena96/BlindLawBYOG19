using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OnClickHandler : MonoBehaviour
{
    [SerializeField] private UnityEngine.Events.UnityEvent onObjectClicked = new UnityEngine.Events.UnityEvent();

    public void OnMouseDown()
    {
        onObjectClicked?.Invoke();
    }
}
