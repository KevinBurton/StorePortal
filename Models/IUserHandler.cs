using System;

namespace Models
{
    public interface IUserHandler
    {
        event Events.QuitEventHandler Quit;
        event Events.ErrorEventHandler Error;
        event Events.SaleEventHandler Sale;
        event Events.ReportEventHandler Report;
        void OnQuit(EventArgs e);
        void OnError(ErrorEventArgs e);
        void OnSale(SaleEventArgs e);
        void OnReport(ReportEventArgs e);
    }
}
