using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Core.Events
{
    /// <summary>
    /// A basic MonoBehaviour-based Event listener. Can be attached to GameObjects.
    /// </summary>
    public class EventListener : MonoBehaviour, IEventListener
    {
        /// <summary>
        /// Event to register with.
        /// </summary>
        [Tooltip("Event to register with.")]
        public Event Event;

        /// <summary>
        /// Response to invoke when Event is raised.
        /// </summary>
        [Tooltip("Response to invoke when Event is raised.")]
        public UnityEvent Response;
        public UnityEvent<List<object>> ResponseWithParameters;

        protected virtual void Awake()
        {
            if (Response == null)
            {
                Response = new UnityEvent();
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

        public virtual void OnEventRaised()
        {
            Response?.Invoke();
        }

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

        public void OnEventRaisedWithParameters(List<object> parameters)
        {
             ResponseWithParameters?.Invoke(parameters);
        }
    }
}