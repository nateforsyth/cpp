using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelegatesAndEventsDemo
{
    // custom event arguments deriving from standard EventArgs
    public class ValueChangedEventArgs : EventArgs
    {
        public object OldValue { get; set; }
        public object NewValue { get; set; }
    }
}
