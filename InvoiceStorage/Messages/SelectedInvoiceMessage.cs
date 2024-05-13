using InvoiceStorage.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceStorage.Messages
{
    public class SelectedInvoiceMessage
    {
        public Invoice Invoice { get; set; }

        public SelectedInvoiceMessage(Invoice invoice)
        {
            Invoice = invoice;
        }
    }
}
