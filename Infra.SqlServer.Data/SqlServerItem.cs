using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.SqlServer.Data
{
    public class SqlServerItem
    {
        public SqlServerItem(Guid orderId, string productName, decimal price, int quantity, decimal total)
        {
            OrderId = orderId;
            ProductName = productName;
            Price = price;
            Quantity = quantity;
            Total = total;
        }

        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid OrderId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal Total { get; set; }
    }
}
