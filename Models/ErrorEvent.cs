namespace Models
{
    public class ErrorEvent : IProcessEvent
    {
        public string Message { get; set; }
        public ErrorEvent(string message)
        {
            this.Message = message;
        }
    }
}
