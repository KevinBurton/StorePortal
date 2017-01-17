using System;

namespace Models
{
    public class UserHandler : IUserHandler
    {
        // An event that clients can use to be notified whenever the
        // elements of the list change.
        public event Events.QuitEventHandler Quit;
        public event Events.ErrorEventHandler Error;
        public event Events.SaleEventHandler Sale;
        public event Events.ReportEventHandler Report;
        public void OnQuit(EventArgs e)
        {
            Quit?.Invoke(this, e);
        }
        public virtual void OnError(ErrorEventArgs e)
        {
            Error?.Invoke(this, e);
        }
        public virtual void OnSale(SaleEventArgs e)
        {
            Sale?.Invoke(this, e);
        }
        public virtual void OnReport(ReportEventArgs e)
        {
            Report?.Invoke(this, e);
        }
    }
}
