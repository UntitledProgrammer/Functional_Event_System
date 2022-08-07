using System;
using System.Collections.Generic;
using System.Text;

namespace np.events
{
    /// <summary>The 'nameless-programmer' functional event system. A system where delegate's are stored
    /// and invoked under a specified 'event-type'.</summary>
    public class EventSystem
    {
        //Attributes:
        public delegate void EventFunction(object data);
        private static EventSystem default_singleton;
        private Dictionary<Type, List<EventFunction>> subscribers;

        //Properties:
        /// <summary>Returns a 'singleton' instance of the Event-System class.</summary>
        public static EventSystem singleton
        {
            get
            {
                if (default_singleton == null) default_singleton = new EventSystem();
                return singleton;
            }
        }

        //Constructors:
        public EventSystem() { subscribers = new Dictionary<Type, List<EventFunction>>(); }

        //Methods:

        /// <summary>The event system keeps a delegate of the specified subscriber, 
        /// which is invoked when an event of the same event type occurs.summary>
        public void Subscribe<EventType>(EventFunction subscriber)
        {
            //If list for the specified event type has not been initialised, define and initalise a new list.
            if(!subscribers.ContainsKey(typeof(EventType))) { subscribers.Add(typeof(EventType), new List<EventFunction>()); }

            subscribers[typeof(EventType)].Add(subscriber); //Store delegate under it's specified 'event-type'.
        }

        /// <summary>Triggers all delegates subscribed to the specified event type.</summary>
        public void Trigger<EventType>(EventType event_data)
        {
            if (!subscribers.ContainsKey(typeof(EventType))) return; //If no delegates are subscribed to specified the event-type, exit method.

            foreach(EventFunction subscriber in subscribers[typeof(EventType)])
            {
                subscriber.Invoke(event_data); //Invoke each method subscribed to specified event type.
            }
        }
    }
}