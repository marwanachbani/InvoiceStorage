using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceStorage.Messages
{
    public class TaxRedcShipMessage
    {
        public TaxRedcShipMessage(decimal taxPercent, decimal redcPercent, string shippingMethod, string shippingTo, decimal shippingAmount)
        {
            TaxPercent = taxPercent;
            RedcPercent = redcPercent;
            ShippingMethod = shippingMethod;
            ShippingTo = shippingTo;
            ShippingAmount = shippingAmount;
        }

        public decimal TaxPercent { get; set; }
        public decimal RedcPercent { get; set; }
        public string ShippingMethod { get; set; }
        public string ShippingTo { get; set; }
        public decimal ShippingAmount { get; set; }
    }
}
