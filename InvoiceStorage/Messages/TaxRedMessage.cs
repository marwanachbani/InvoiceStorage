using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceStorage.Messages
{
    public class TaxRedMessage
    {
        public TaxRedMessage(decimal taxPercent, decimal redcPercent)
        {
            TaxPercent = taxPercent;
            RedcPercent = redcPercent;
        }

        public decimal TaxPercent { get; set; }
        public decimal RedcPercent { get; set; }
    }
}
