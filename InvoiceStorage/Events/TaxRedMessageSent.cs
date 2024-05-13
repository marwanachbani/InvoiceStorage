using CommunityToolkit.Mvvm.Messaging.Messages;
using InvoiceStorage.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceStorage.Events
{
    public class TaxRedMessageSent : ValueChangedMessage<TaxRedMessage>
    {
        public TaxRedMessageSent(TaxRedMessage value) : base(value)
        {
        }
    }
}
