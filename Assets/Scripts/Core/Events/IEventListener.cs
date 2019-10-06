using System.Collections.Generic;

namespace Core.Events
{
    /// <summary>
    /// An interface for Event Listeners.
    /// </summary>
    public interface IEventListener
    {
        void OnEventRaised();
        void OnEventRaisedWithParameters(List<object> parameters);    // TODO: Will discuss
    }
}