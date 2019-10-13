using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CollisionCaller : MonoBehaviour
{
    [SerializeField] private UnityEngine.Events.UnityEvent onCollisionEnter = new UnityEngine.Events.UnityEvent();
    [SerializeField] private UnityEngine.Events.UnityEvent onCollisionExit = new UnityEngine.Events.UnityEvent();

    private void OnTriggerEnter2D(Collider2D other)
    {
        onCollisionEnter?.Invoke();
    }
}
