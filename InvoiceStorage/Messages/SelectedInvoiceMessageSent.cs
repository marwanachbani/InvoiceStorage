using CommunityToolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceStorage.Messages
{
    public class SelectedInvoiceMessageSent : ValueChangedMessage<SelectedInvoiceMessage>
    {
        public SelectedInvoiceMessageSent(SelectedInvoiceMessage value) : base(value)
        {
        }
    }
}
