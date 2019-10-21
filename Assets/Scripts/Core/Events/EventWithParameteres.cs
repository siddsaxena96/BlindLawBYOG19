using System.Collections.Generic;
using UnityEngine;

namespace Core.Events
{
/// <summary>
    /// A asset-based Event which acts as a hybrid between C# events and UnityEvents. Utilizes ScriptableObject asset ID hash as the reference.
    /// </summary>
    [CreateAssetMenu]
    public class EventWithParameteres : ScriptableObject
    {
#if UNITY_EDITOR
        /// <summary>
        /// Developer description of the Event.
        /// </summary>
        [Tooltip(" Developer description of the Event.")]
        [Multiline]
        [SerializeField]
        private string developerDescription = "";
        internal string DeveloperDescription => developerDescription;

        /// <summary>
        /// Should debug be shown?
        /// </summary>
        [Tooltip("Should debug be shown?")]
        [SerializeField]
        private bool showDebug = false;
#endif

        /// <summary>
        /// The list of listeners that this event will notify if it is raised.
        /// </summary>
        private readonly List<IEventListener> eventListenersWithParameters = new List<IEventListener>();

        /// <summary>
        /// Raises the Event.
        /// </summary>
        public virtual void Raise(List<object> parameters)
        {
#if UNITY_EDITOR
            if (showDebug)
            {
                Debug.Log(string.Format("[{0}] Event [{1}] raised", System.DateTime.Now, name));
            }
#endif
            for (int i = eventListenersWithParameters.Count - 1; i >= 0; i--)
            {
                eventListenersWithParameters[i]?.OnEventRaisedWithParameters(parameters);
            }
        }

        /// <summary>
        /// Registers the event listener to the invocation list.
        /// </summary>
        /// <param name="listener">The EventListener to register.</param>
        public void RegisterListener(IEventListener listener)
        {
            if (!eventListenersWithParameters.Contains(listener))
            {
                eventListenersWithParameters.Add(listener);
            }
        }

        /// <summary>
        /// Un-registers the event listener from the invocation list.
        /// </summary>
        /// <param name="listener">The EventListener to un-register.</param>
        public void UnregisterListener(IEventListener listener)
        {
            if (eventListenersWithParameters.Contains(listener))
            {
                eventListenersWithParameters.Remove(listener);
            }
        }

        /// <summary>
        /// The number of active listeners.
        /// </summary>
        public int ActiveListenerCount => eventListenersWithParameters.Count;
    }
}