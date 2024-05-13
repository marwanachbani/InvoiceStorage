using CommunityToolkit.Mvvm.Messaging.Messages;
using Infra.SqlServer.Data;
using InvoiceStorage.Messages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceStorage.Events
{
    public class SqlServerInvoicesSent : ValueChangedMessage<SqlServerInvoicesMessage>
    {
        public SqlServerInvoicesSent(SqlServerInvoicesMessage value) : base(value)
        {
        }
    }
}
