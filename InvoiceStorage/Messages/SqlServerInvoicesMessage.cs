using Infra.SqlServer.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceStorage.Messages
{
    public class SqlServerInvoicesMessage
    {
        public ObservableCollection<SqlServerOrderInvoice> Items { get; set; }

        public SqlServerInvoicesMessage(ObservableCollection<SqlServerOrderInvoice> items)
        {
            Items = items;
        }
    }
}
