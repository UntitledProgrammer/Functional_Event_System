using System;
using np.events;

namespace np.examples
{
    class Player
    {
        //Methods:
        static public void Speak(object message)
        {
            Console.WriteLine((string)message);
        }
    }

    class Debug
    {
        //Methods:
        public void Log(object message)
        {
            Console.WriteLine("Debug: " + (string)message);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //Declare new event system.
            EventSystem system = new EventSystem();

            Debug debug = new Debug();

            //Subscribe methods/functions.
            system.Subscribe<string>(Player.Speak); //Static.
            system.Subscribe<string>(debug.Log); //Instance.

            //Trigger an event.
            system.Trigger<string>("Hello World."); //Invokes all delegates subscribed to the string event type.
            system.Trigger<bool>(true); //Ignores bool event type since no delegates have been subscribed to typeof(bool).
        }
    }
}
