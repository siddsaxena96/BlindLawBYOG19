using System;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Delegate for Precondition events.
/// </summary>
/// <param name="precondition">Precondition argument.</param>
public delegate void PreconditionEventHandler(Precondition precondition);

[Serializable]
[CreateAssetMenu(fileName = "NewPrecondition", menuName = "Data/Precondition")]
public class Precondition : ScriptableObject
{
#if UNITY_EDITOR
    /// <summary>
    /// Developer description of the Precondition.
    /// </summary>
    [Tooltip(" Developer description of the Precondition.")]
    [Multiline]
    [SerializeField]
    private string developerDescription = "";
    internal string DeveloperDescription => developerDescription;
#endif

    [SerializeField]
    private bool isFulfilledByDefault = false;

    [SerializeField]
    private bool isFulfilled = false;

    private event PreconditionEventHandler OnFulfilled;
    private event PreconditionEventHandler OnUnfulfilled;

    /// <summary>
    /// Is the Precondition fulfilled?
    /// </summary>
    public bool IsFulfilled
    {
        get => isFulfilled;
        private set
        {
            if (isFulfilled == value)
            {
                return;
            }

            PreconditionManager.Instance.AddPrecondition(this);

            isFulfilled = value;

            if (isFulfilled)
            {
                OnFulfilled?.Invoke(this);
            }
            else
            {
                OnUnfulfilled?.Invoke(this);
            }
        }
    }

    /// <summary>
    /// Is the Precondition fulfilled by default?
    /// </summary>
    public bool IsFulfilledByDefault => isFulfilledByDefault;

    private void OnEnable()
    {
        isFulfilled = isFulfilledByDefault;
    }

    private void OnDisable()
    {
        Reset();
        //PreconditionManager.Instance.RemovePrecondition(this);
    }

    /// <summary>
    /// Sets the fulfillment state of the Precondition.
    /// </summary>
    /// <param name="isFulfilled">Should the Precondition be fulfilled?</param>
    public void Fulfill(bool isFulfilled)
    {
        IsFulfilled = isFulfilled;
    }

    /// <summary>
    /// Registers an event handler to the OnFulfilled event.
    /// </summary>
    /// <param name="callback">Callback to add.</param>
    public void RegisterFulfilledHandler(PreconditionEventHandler callback)
    {
        PreconditionManager.Instance.AddPrecondition(this);
        OnFulfilled += callback;
    }

    /// <summary>
    /// Unregisters an event handler from the OnFulfilled event.
    /// </summary>
    /// <param name="callback">Callback to remove.</param>
    public void UnregisterFulfilledHandler(PreconditionEventHandler callback)
    {
        OnFulfilled -= callback;
    }

    /// <summary>
    /// Registers an event handler to the OnUnfilfilled event.
    /// </summary>
    /// <param name="callback">Callback to add.</param>
    public void RegisterUnfulfilledHandler(PreconditionEventHandler callback)
    {
        PreconditionManager.Instance.AddPrecondition(this);
        OnUnfulfilled += callback;
    }

    /// <summary>
    /// Unregisters an event handler from the OnUnfulfilled event.
    /// </summary>
    /// <param name="callback">Callback to remove.</param>
    public void UnregisterUnfulfilledHandler(PreconditionEventHandler callback)
    {
        OnUnfulfilled -= callback;
    }

    /// <summary>
    /// Returns the registered handler count of the OnFulfilled event.
    /// </summary>
    /// <returns></returns>
    public int GetFulfilledHandlerCount()
    {
        return GetHandlerCount(OnFulfilled);
    }

    /// <summary>
    /// Returns the registered handler count of the OnUnfulfilled event.
    /// </summary>
    /// <returns></returns>
    public int GetUnfulfilledHandlerCount()
    {
        return GetHandlerCount(OnUnfulfilled);
    }

    /// <summary>
    /// Retrieves the registered handler count of the specified delegate.
    /// </summary>
    /// <param name="delegate">Delegate to retrieve handler count of.</param>
    /// <returns>Registered handler count</returns>
    public static int GetHandlerCount(Delegate @delegate)
    {
        return @delegate != null ? @delegate.GetInvocationList().Length : 0;
    }

    /// <summary>
    /// Resets the Precondition's state and its Events.
    /// </summary>
    public void Reset()
    {
        OnFulfilled = null;
        OnUnfulfilled = null;
        isFulfilled = IsFulfilledByDefault;
    }

    //used to reset the Precondition's value to its original value so it doesn't carry over its value from the previous scene
    private void SceneManager_activeSceneChanged(Scene arg0, Scene arg1)
    {
        Reset();
    }

#if UNITY_EDITOR
    [ContextMenu("Fulfill")]
    private void FulfillContext()
    {
        Fulfill(true);
    }

    [ContextMenu("Fulfill", true)]
    private bool FulfillContextValidate()
    {
        return Application.isPlaying;
    }

    [ContextMenu("Unfulfill")]
    private void UnfulfillContext()
    {
        Fulfill(false);
    }

    [ContextMenu("Unfulfill", true)]
    private bool UnfulfillContextValidate()
    {
        return Application.isPlaying;
    }

    [ContextMenu("Count OnFulfilled delegates")]
    private void CountInvocationsForFulfilledContext()
    {
        if (OnFulfilled != null)
        {
            Debug.Log(string.Format("{0} OnFulfilled event has {1} delegate(s) registered", name, OnFulfilled.GetInvocationList().Length));
        }
    }

    [ContextMenu("Count OnUnfulfilled delegates")]
    private void CountInvocationsForUnfulfilledContext()
    {
        if (OnUnfulfilled != null)
        {
            Debug.Log(string.Format("{0} OnUnfulfilled event has {1} delegate(s) registered", name, OnUnfulfilled.GetInvocationList().Length));
        }
    }
#endif
}
