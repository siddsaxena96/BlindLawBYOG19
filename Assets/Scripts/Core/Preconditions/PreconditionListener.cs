using UnityEngine;
using UnityEngine.Events;

public class PreconditionListener : MonoBehaviour
{
    [SerializeField] private Precondition precondition = null;

    [SerializeField] private UnityEvent onPreconditionFulfilled = new UnityEvent();
    [SerializeField] private UnityEvent onPreconditionUnfulfilled = new UnityEvent();

    private void OnEnable()
    {
        precondition?.RegisterFulfilledHandler(PreconditionEventHandler);
        precondition?.RegisterUnfulfilledHandler(PreconditionEventHandler);
    }

    private void OnDisable()
    {

        precondition?.UnregisterFulfilledHandler(PreconditionEventHandler);
        precondition?.UnregisterUnfulfilledHandler(PreconditionEventHandler);
    }

    private void PreconditionEventHandler(Precondition precondition)
    {
        if (precondition.IsFulfilled)
        {
            onPreconditionFulfilled?.Invoke();
        }
        else
        {
            onPreconditionUnfulfilled?.Invoke();
        }
    }
}
