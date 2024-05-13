using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraSqlLite.Data
{
    public class SqliteOrderInvoice
    {
        public Guid Id { get; set; }
        public string Customer { get; set; }
        public DateTime Date { get; set; }
        public List<SqliteItem> Items { get; set; }
        public decimal SubTotal { get; set; }
        public bool WithShipping { get; set; }
        public string ShippingMethod { get; set; }
        public string ShippingTo { get; set; }
        public decimal ShippingAmount { get; set; }
        public decimal TaxPercent { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal ReductionPercent { get; set; }
        public decimal ReductionAmount { get; set; }
        public decimal Total { get; set; }
    }
}
