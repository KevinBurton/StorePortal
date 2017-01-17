using System;

namespace Models
{
    public class ErrorEventArgs : EventArgs
    {
        public string Message { get; set; }
        public ErrorEventArgs(string message)
        {
            this.Message = message;
        }
    }
}
