﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.SqlServer.Data
{
    public class SqlServerOrderInvoice
    {
        public SqlServerOrderInvoice(Guid id, string customer, DateTime date, decimal subTotal, bool withShipping, string shippingMethod, string shippingTo, decimal shippingAmount, decimal taxPercent, decimal taxAmount, decimal reductionPercent, decimal reductionAmount, decimal total)
        {
            Id = id;
            Customer = customer;
            Date = date;
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
        }
        public SqlServerOrderInvoice()
        {
            
        }
        public Guid Id { get; set; }
        public string Customer { get; set; }
        public DateTime Date { get; set; }
        public List<SqlServerItem> Items { get; set; } = new List<SqlServerItem>();
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
