using Infra.Sqlite.Databases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceStorage.Messages
{
    public class DatabaseMessage
    {
        public Databases Database { get; set; }

        public DatabaseMessage(Databases database)
        {
            Database = database;
        }
    }
}
