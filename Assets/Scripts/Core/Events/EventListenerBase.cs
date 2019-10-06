using System.Collections.Generic;
using UnityEngine;

namespace Core.Events
{
    /// <summary>
    /// An abstract EventListener base class.
    /// </summary>
    /// <typeparam name="T">Type of Component.</typeparam>
    public abstract class EventListenerBase<T> : MonoBehaviour, IEventListener where T : Component
    {
        [Tooltip("The Component type.")]
        [SerializeField] protected T component;

        /// <summary>
        /// Event to register with.
        /// </summary>
        [Tooltip("Event to register with.")]
        public Event Event;

        protected virtual void Awake()
        {
            if (component == null)
            {
                component = GetComponent<T>();
                if (component == null)
                {
                    Debug.LogError(typeof(T).Name + " component not found, disabling behaviour.");
                    enabled = false;
                    return;
                }
            }
        }

        protected virtual void OnEnable()
        {
            Register();
        }

        protected virtual void OnDisable()
        {
            Unregister();
        }

        /// <summary>
        /// Invoked when the Event is raised.
        /// </summary>
        public abstract void OnEventRaised();

        /// <summary>
        /// Registers this listener to the event.
        /// </summary>
        public void Register()
        {
            Event?.RegisterListener(this);
        }

        /// <summary>
        /// Unrgisters this listener from the event.
        /// </summary>
        public void Unregister()
        {
            Event?.UnregisterListener(this);
        }

        public abstract void OnEventRaisedWithParameters(List<object> parameters);
    }
}
