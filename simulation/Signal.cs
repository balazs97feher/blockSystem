using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simulation
{
    public enum SignalState
    {
        Red,
        Green,
        White,
        Blank
    }

    public class Signal : Field
    {
        public SignalState State;
        public Signal(int x, int y) : base(x,y)
        {
            State = SignalState.Red;
        }
        

    }
}
