using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDFCHARPConversion.Models
{
    public class InvoiceItem
    {
        public InvoiceItem(string productName, int quantity, decimal price, decimal total)
        {
            ProductName = productName;
            Quantity = quantity;
            Price = price;
            Total = total;
        }

        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Total { get; set; }
    }
}
