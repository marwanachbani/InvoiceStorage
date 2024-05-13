using CommunityToolkit.Mvvm.Messaging.Messages;
using InvoiceStorage.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceStorage.Events
{
    public class InvoiceInfoMessageSent : ValueChangedMessage<InvoiceInfoMessage>
    {
        public InvoiceInfoMessageSent(InvoiceInfoMessage value) : base(value)
        {
        }
    }
}
