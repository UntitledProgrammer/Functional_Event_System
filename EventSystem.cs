using System;
using System.Collections.Generic;
using System.Text;

namespace EventSystem
{
    public class EventSystem
    {
        //Attributes:
        public delegate void Event(object data);
        private static EventSystem default_singleton;
        private Dictionary<Type, List<Event>> subscribers;

        //Properties:
        public static EventSystem singleton
        {
            get
            {
                if (default_singleton == null) default_singleton = new EventSystem();
                return singleton;
            }
        }

        //Constructors:
        public EventSystem() { subscribers = new Dictionary<Type, List<Event>>(); }

        //Methods:

        /// <summary>The event system keeps a delegate of the specified subscriber, 
        /// which is invoked when an event of the same event type occurs.summary>
        public void Subscribe<EventType>(Event subscriber)
        {
            //If list for the specified event type has not been initialised, define and initalise a new list.
            if(!subscribers.ContainsKey(typeof(EventType))) { subscribers.Add(typeof(EventType), new List<Event>()); }

            subscribers[typeof(EventType)].Add(subscriber);
        }

        /// <summary>Triggers all delegates subscribed to the specified event type.</summary>
        public void Trigger<EventType>(EventType event_data)
        {
            foreach(Event subscriber in subscribers[typeof(EventType)])
            {
                subscriber.Invoke(event_data); //Invoke each method subscribed to specified event type.
            }
        }
    }
}
