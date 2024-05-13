using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using InvoiceStorage.Events;
using InvoiceStorage.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceStorage.ViewModel
{
    public partial class AddingInvoiceTpDaabaseInfoVM : ObservableRecipient
    {
        [ObservableProperty]
        private string costumer;
        [ObservableProperty]
        private DateTime date = DateTime.Now;
        [RelayCommand]
        public void Next()
        {
            if(!string.IsNullOrEmpty(Costumer) ) {
                var message = new InvoiceInfoMessage(Costumer, Date);
                var @event = new InvoiceInfoMessageSent(message);
                Messenger.Send(@event);
            }
        }
        [RelayCommand]
        public void Cancel()
        {

        }
    }
}
