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
    public partial class AddingInvoiceToDatabaseTaxRedcVM : ObservableRecipient
    {
        [ObservableProperty]
        private decimal taxPercent;
        
        [ObservableProperty]
        private decimal redcPercent;
       
        [ObservableProperty]
        private string shippingMethod;
        [ObservableProperty]
        private string shippingTo;
        [ObservableProperty]
        private decimal shippingAmount;
        [ObservableProperty]
        private bool isShipped;
        [RelayCommand]
        public void Finish()
        {
            if(IsShipped==true)
            {
                if(!string.IsNullOrEmpty(ShippingMethod) && !string.IsNullOrEmpty(ShippingTo)) 
                {
                    var message = new TaxRedcShipMessage(TaxPercent, RedcPercent, ShippingMethod, ShippingTo, ShippingAmount);
                    var @event = new TaxRedcShipMessageSent(message);
                    Messenger.Send(@event);
                }
            }
            else
            {
                var message = new TaxRedMessage(TaxPercent, RedcPercent);
                var @event = new TaxRedMessageSent(message);
                Messenger.Send(@event);
            }
        }
        [RelayCommand]
        public void Previous()
        {
            Messenger.Send(new TaxRedPreviousClicked());
        }
    }
}
