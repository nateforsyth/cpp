using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelegatesAndEventsDemo
{
    /// <summary>
    /// EventArgs for PropertyChanged event that also contains previous and new value
    /// </summary>
    public class PropertyValueChangedEventArgs : PropertyChangedEventArgs
    {
        /// <summary>
        /// Create a new PropertyValueChanged EventArgs
        /// </summary>
        /// <param name="propertyName">The name of the property that changed</param>
        /// <remarks>OldValue and NewValue default to null</remarks>
        public PropertyValueChangedEventArgs(String propertyName) : this(propertyName, null, null) { }

        /// <summary>
        /// Create a new PropertyValueChanged EventArgs
        /// </summary>
        /// <param name="propertyName">The name of the property that changed</param>
        /// <param name="OldValue">The previous value of the property</param>
        /// <param name="NewValue">The new value of the property</param>
        public PropertyValueChangedEventArgs(String propertyName, object OldValue, object NewValue) : base(propertyName)
        {
            this.OldValue = OldValue;
            this.NewValue = NewValue;
        }

        /// <summary>
        /// Previous value of the property
        /// </summary>
        public object OldValue { get; private set; }
        /// <summary>
        /// New value of the property
        /// </summary>
        public object NewValue { get; private set; }
    }
}
