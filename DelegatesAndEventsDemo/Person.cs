using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelegatesAndEventsDemo
{
    // basic class to create objects
    // implements event handler to notify of changes to the object structure
    public class Person
    {
        public event ValueChangedEventHandler<ValueChangedEventArgs> ValueChanged;

        private string firstName;
        private string lastName;
        private int age;

        public override string ToString()
        {
            return string.Format("{0} {1} is {2} years old.", firstName, lastName, age);
        }

        public string FirstName
        {
            get { return firstName; }
            set
            {
                if (firstName != value)
                {
                    var oldValue = firstName;
                    firstName = value;

                    if (ValueChanged != null)
                    {
                        ValueChangedEventArgs args = new ValueChangedEventArgs();
                        args.OldValue = oldValue;
                        args.NewValue = firstName;
                        ValueChanged(this, args);
                    }
                }
            }
        }

        public string LastName
        {
            get { return lastName; }
            set
            {
                if (lastName != value)
                {
                    var oldValue = lastName;
                    lastName = value;

                    if (ValueChanged != null)
                    {
                        ValueChangedEventArgs args = new ValueChangedEventArgs();
                        args.OldValue = oldValue;
                        args.NewValue = lastName;
                        ValueChanged(this, args);
                    }
                }
            }
        }

        public int Age
        {
            get { return age; }
            set
            {
                if (age != value)
                {
                    var oldValue = age;
                    age = value;

                    if (ValueChanged != null)
                    {
                        ValueChangedEventArgs args = new ValueChangedEventArgs();
                        args.OldValue = oldValue;
                        args.NewValue = age;
                        ValueChanged(this, args);
                    }
                }
            }
        }
    }
}
