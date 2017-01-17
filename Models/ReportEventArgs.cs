using System;
using System.Collections.Generic;

namespace Models
{
    public class ReportEventArgs : EventArgs
    {
        public Stack<string> ReportStack { get; set; }
    }
}
