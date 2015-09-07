using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelegatesAndEventsDemo
{
    /// <summary>
    /// basic class to create objects
    /// implements event handler to notify of changes to the object structure
    /// </summary>
    public class Person : INotifyPropertyChanged
    {
        /// <summary>
        /// PropertyChanged event. Fires when a property is changed
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        private string firstName;
        private string lastName;
        private int age;

        /// <summary>
        /// Create a new Person
        /// </summary>
        /// <param name="FirstName">First name, default: ""</param>
        /// <param name="LastName">Last name, default: ""</param>
        /// <param name="Age">Age, default: 0</param>
        public Person(String FirstName = "", String LastName = "", int Age = 0)
        {
            // Set the fields not the properties to avoid the unneccessary code path for event handling
            this.firstName = FirstName;
            this.lastName = LastName;
            this.age = Age;
        }

        /// <summary>
        /// The firstname of a person
        /// </summary>
        public string FirstName
        {
            get { return this.firstName; }
            set
            {
                if (this.firstName != value)
                {
                    // Because the changes are invocated by the TaskScheduler, we get race conditions
                    // We need to set the properties before calling the event
                    // Otherwise calls to ToString will show wrong values, but more strangely sometimes
                    // they will show correct values. Depending on when things are called
                    var old = this.firstName;
                    this.firstName = value;
                    OnPropertyValueChanged("FirstName", old, value);
                }
            }
        }


        /// <summary>
        /// The surname of a person
        /// </summary>
        public string LastName
        {
            get { return this.lastName; }
            set
            {
                if (this.lastName != value)
                {
                    var old = this.lastName;
                    this.lastName = value;
                    OnPropertyValueChanged("LastName", old, value);
                }
            }
        }

        /// <summary>
        /// The age of a person
        /// </summary>
        public int Age
        {
            get { return this.age; }
            set
            {
                // If the value we are setting is different to the value we have now
                if (this.age != value)
                {
                    var old = this.age;
                    this.age = value;
                    OnPropertyValueChanged("Age", old, value);
                }
            }
        }

        /// <summary>
        /// Call to signal a change on a property. Will fire PropertyChanged event
        /// </summary>
        /// <param name="propertyName">The name of a the property that changed</param>
        /// <param name="oldValue">The old value</param>
        /// <param name="newValue">The new value</param>
        protected void OnPropertyValueChanged(String propertyName, object oldValue, object newValue)
        {
            // If there are no event listeners, we noop
            if(PropertyChanged != null)
            {
                // Construct our arguments to dispatch
                var args = new PropertyValueChangedEventArgs(propertyName, oldValue, newValue);
                // In an asyncronous environment, we have the option to await on this invocation
                // The way it's implemented now is Fire and Forget. The calling of the handler will be dispatched bo the TaskScheduler
                var asyncInvocation = Task.Factory.FromAsync<object, PropertyChangedEventArgs>(PropertyChanged.BeginInvoke, PropertyChanged.EndInvoke, this, args, null, TaskCreationOptions.None);
                // We attach a continution that will only fire if the target event handler faulted (Threw an exception)
                // This allows us to log it safely. If we had a logger that is.
                asyncInvocation.ContinueWith(async (task) => // Marked as async to allow awaiting in the Func body
                {
                    // Just print everything to stderr (Error output on the Console)
                    await Console.Error.WriteLineAsync("Invocation of PropertyChangedHandler faulted");
                    // Cautionary null check.
                    if(task.Exception != null)
                    {
                        // Iterate all the exceptions in this aggregate exception and send them to Console asyncronously
                        foreach(var innerException in task.Exception.InnerExceptions)
                        {
                            await Console.Error.WriteLineAsync(innerException.Message);
                        }
                    }
                }, TaskContinuationOptions.OnlyOnFaulted); // Only runs the continuation in the case of an error
            }
        }

        /// <summary>
        /// String representation of this object
        /// </summary>
        /// <returns>String describing this object, useful for debugging and logging</returns>
        public override string ToString()
        {
            return string.Format("{0} {1} is {2} years old.", firstName, lastName, age);
        }

        /// <summary>
        /// Calculate a hashcode unique to this object
        /// </summary>
        /// <returns>A unique int based on the firstname, lastname and age fields</returns>
        public override int GetHashCode()
        {
            /*
             Create a Tuple (It's like an array of mixed types) that will calculate a hashcode for us
             Hashcodes are the same when all the "describing factors" of an object are the same
             In this case we pass the firstName, lastName and age in to a Tuple and let that class do the calculation
             
             Taken from the Examples section of this article on MSDN: https://msdn.microsoft.com/en-us/library/system.object.gethashcode.aspx
             Tuples are also order specific, so changing the order of the fields will change the resulting hashcode.
             Something of note also; is this extract:
             "Note, though, that the performance overhead of instantiating a Tuple object may significantly impact
              the overall performance of an application that stores large numbers of objects in hash tables."

             I don't think that will be much of an issue in this project though. 
             - Joshua Lloyd 20150907
            */
            return Tuple.Create(firstName, lastName, age).GetHashCode();
        }
    }
}
