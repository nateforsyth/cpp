#define BASIC

using System;
using System.Collections.Generic;
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
            Person p = new Person() { FirstName = "Nathan", LastName = "Forsyth", Age = 100 };
            Console.WriteLine(p.ToString() + "\r\n");

            p.ValueChanged += p_ValueDelegate; // program is now subscribed to the value change event handler

            p.FirstName = "John";
            p.LastName = "Wang";
            p.Age = 99;

            Console.WriteLine();

            p.FirstName = "Ray";
            p.LastName = "Singer";
            p.Age = 98;

            Console.Read();
        }

        private static void p_ValueDelegate(object sender, ValueChangedEventArgs args)
        {
#if BASIC // comment/uncomment first line to switch logic path
            // basic event handling
            Console.WriteLine("{0} changed to {1}", args.OldValue, args.NewValue);
            Console.WriteLine(sender.ToString()); // call overridden ToString() on the sender object (person in this case)
#else
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
#endif
        }
    }
}
