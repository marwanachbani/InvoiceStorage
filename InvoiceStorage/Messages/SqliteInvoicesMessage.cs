using InfraSqlLite.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceStorage.Messages
{
    public class SqliteInvoicesMessage
    {
        public ObservableCollection<SqliteOrderInvoice> Items { get; set; }

        public SqliteInvoicesMessage(ObservableCollection<SqliteOrderInvoice> items)
        {
            Items = items;
        }
    }
}
