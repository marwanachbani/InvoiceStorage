using CommunityToolkit.Mvvm.Messaging.Messages;
using InvoiceStorage.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceStorage.Events
{
    public class DatabaseMessageSent : ValueChangedMessage<DatabaseMessage>
    {
        public DatabaseMessageSent(DatabaseMessage value) : base(value)
        {
        }
    }
}
