using System;
using System.Collections.Generic;

namespace Core.Events
{
    /// <summary>
    /// A Scriptable EventListener that can be created with the new() constructor.
    /// </summary>
    public class ScriptableEventListener : IEventListener
    {
        /// <summary>
        /// Event handler for when the Event is raised.
        /// </summary>
        public event EventHandler EventRaised;
        public event EventHandler EventRaisedWithParameters;

        /// <summary>
        /// Raises the EventHandler when invoked.
        /// </summary>
        public void OnEventRaised()
        {
            EventRaised?.Invoke(this, null);
        }

        public void OnEventRaisedWithParameters(List<object> parameters)
        {
            EventRaisedWithParameters?.Invoke(this, null);
        }

        ~ScriptableEventListener()
        {

        }
    }
}
