using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelegatesAndEventsDemo
{
    // delegate for event handler - ensures that the arguments derive from EventArgs
    public delegate void ValueChangedEventHandler<T>(object sender, T args) where T : EventArgs;
}
