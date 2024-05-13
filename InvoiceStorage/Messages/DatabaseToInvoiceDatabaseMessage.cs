using Infra.Sqlite.Databases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceStorage.Messages
{
    public class DatabaseToInvoiceDatabaseMessage
    {
        public DatabaseToInvoiceDatabaseMessage(Databases database)
        {
            Database = database;
        }

        public Databases Database { get; set; }
    }
}
