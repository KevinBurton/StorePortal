using System;

namespace Models
{
    public class Events
    {
        public delegate void QuitEventHandler(object sender, EventArgs e);
        public delegate void ErrorEventHandler(object sender, ErrorEventArgs e);
        public delegate void SaleEventHandler(object sender, SaleEventArgs e);
        public delegate void ReportEventHandler(object sender, ReportEventArgs e);
    }
}
