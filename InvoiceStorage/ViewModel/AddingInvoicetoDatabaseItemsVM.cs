using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using InvoiceStorage.Events;
using InvoiceStorage.Messages;
using InvoiceStorage.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceStorage.ViewModel
{
    public partial class AddingInvoicetoDatabaseItemsVM : ObservableRecipient
    {
        [ObservableProperty]
        private string productName;
        [ObservableProperty]
        private int quantity;
        [ObservableProperty]
        private decimal price;
        [ObservableProperty]
        private ObservableCollection<Item> itemsinvoice = new();
        [ObservableProperty]
        [NotifyPropertyChangedRecipients]

        private Item selectedItem = null;
        [RelayCommand]
        public void AddItemTotable()
        {
            Itemsinvoice.Add(new Item(ProductName, Quantity, Price));     
        }
        [RelayCommand]
        public void RemoveItemTotable() 
        {
            Itemsinvoice.Remove(SelectedItem);
        }
        [RelayCommand]
        public void Next()
        {
            if(Itemsinvoice!=null)
            {
                var message = new InvoiceItemsMeesage(Itemsinvoice);
                var @event = new InvoiceItemsMessageSent(message);
                Messenger.Send(@event);
            }
        }
        [RelayCommand]
        public void Previous()
        {
            Messenger.Send(new ItemsPreviousClicked());
        }

    }
}
