#define BASIC

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DelegatesAndEventsDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            // create a person and display the parameters to the console
            var p = new Person("Nathan", "Forsyth", 100);
            LogPerson(p);

            p.PropertyChanged += p_ValueDelegate; // program is now subscribed to the value change event handler

            p.FirstName = "John";
            p.LastName = "Wang";
            p.Age = 99;
            LogPerson(p);

            p.FirstName = "Ray";
            p.LastName = "Singer";
            p.Age = 98;
            LogPerson(p);

            Console.Read();
        }

        /// <summary>
        /// Logs a person object, also handles null
        /// </summary>
        /// <param name="p">Person to log to Console</param>
        static void LogPerson(Person p)
        {
            if (p == null)
            {
                Console.WriteLine("LogPerson(null)\r\n");
            }
            else
            {
                Console.WriteLine("{0}\r\n", p);
            }
        }

        // This is invoked asyncronously!!
        private static void p_ValueDelegate(object sender, PropertyChangedEventArgs args)
        {
            // Create a dynamic object that references the arguments
            // This allows us to use .OldValue and .NewValue without casting if the type matches
            // This is probably bad practice or something. 
            dynamic ooohDynamic = args;

            // If we have the values, log them
            if (ooohDynamic is PropertyValueChangedEventArgs)
            {
                Console.WriteLine("Property {0} changed from {1} to {2}", ooohDynamic.PropertyName, ooohDynamic.OldValue, ooohDynamic.NewValue);
            }
            else // Otherwise we only have the property name
            {
                Console.WriteLine("Property {0} changed.", ooohDynamic.PropertyName);
            }

            // Somewhat obsoleted by PropertyChangedEvent. We need to find a new use for Reflection! :D

            /*
            // more advanced event handling; allows for reflection inside the object to get property NAME, the value of which is compared to the argument value
            PropertyInfo[] properties = sender.GetType().GetProperties();
            string changedProperty = "";

            foreach (PropertyInfo property in properties)
            {
                if (property.GetValue(sender).Equals(args.NewValue)) // may be more efficient with a do or while loop
                {
                    changedProperty = property.Name;
                }
            }

            Console.WriteLine("{0} changed from {1} to {2}", changedProperty, args.OldValue, args.NewValue);
            Console.WriteLine(sender.ToString());
            */
        }
    }
}
