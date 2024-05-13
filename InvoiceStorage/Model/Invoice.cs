using Infra.SqlServer.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceStorage.Model
{
    public class Invoice
    {
        public Invoice(Guid id, string customer, List<Item> items, decimal subTotal, bool withShipping, string shippingMethod, string shippingTo, decimal shippingAmount, decimal taxPercent, decimal taxAmount, decimal reductionPercent, decimal reductionAmount, decimal total, DateTime date)
        {
            Id = id;
            Customer = customer;
            Items = items;
            SubTotal = subTotal;
            WithShipping = withShipping;
            ShippingMethod = shippingMethod;
            ShippingTo = shippingTo;
            ShippingAmount = shippingAmount;
            TaxPercent = taxPercent;
            TaxAmount = taxAmount;
            ReductionPercent = reductionPercent;
            ReductionAmount = reductionAmount;
            Total = total;
            Date = date;
        }

        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string Customer { get; set; }
        public List<Item> Items { get; set; }
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
