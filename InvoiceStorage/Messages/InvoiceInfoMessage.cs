using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceStorage.Messages
{
    public class InvoiceInfoMessage
    {
        public InvoiceInfoMessage(string customer, DateTime date)
        {
            Customer = customer;
            Date = date;
        }

        public string Customer { get; set; }
        public DateTime Date { get; set; }
    }
}
