using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using InvoiceStorage.Events;
using InvoiceStorage.Messages;
using InvoiceStorage.Model;
using PDFCHARPConversion;
using WordConvertionNPoi;

namespace InvoiceStorage.ViewModel
{
    public partial class InvoiceDatabaseDetailVM : ObservableRecipient 
    {
        public InvoiceDatabaseDetailVM()
        {
            Messenger.Register<InvoiceDatabaseDetailVM, SelelctedInvoiceToInvoicedeailsSent>(this, (m, t) =>
            {
                GetInvoice(t.Value.Invoice);
            });
        }
        [ObservableProperty]
        private string customer;
        [ObservableProperty]
        private DateTime date;
        [ObservableProperty]
        private string productName;
        [ObservableProperty]
        private int quantity;
        [ObservableProperty]
        private decimal price;
        [ObservableProperty]
        private decimal taxPercent;
        [ObservableProperty]
        private decimal redcPercent;
        [ObservableProperty]
        private decimal redcAmount;
        [ObservableProperty]
        private decimal taxAmount; 
        [ObservableProperty]
        private string shippingMethode;
        [ObservableProperty]
        private string shippingTo;
        [ObservableProperty]
        private decimal shippingAmont;
        [ObservableProperty]
        private bool isShipped;
        [ObservableProperty]
        private decimal subTotal;
        [ObservableProperty]
        private decimal total;
       
        [ObservableProperty]
        private ObservableCollection<Item> itemsinvoice = new();
        [ObservableProperty]
        private string id; 
        private void GetInvoice(Invoice invoice)
        {

            if (invoice != null)
            {
                Id = invoice.Id.ToString();
                Customer = invoice.Customer;
                foreach (var item in invoice.Items)
                {
                    
                    Itemsinvoice.Add(new Item(item.ProductName, item.Quantity, item.Price));
                }
                RedcPercent = invoice.ReductionPercent;
                RedcAmount = invoice.ReductionAmount;
                TaxPercent = invoice.TaxPercent;
                TaxAmount = invoice.TaxAmount;
                IsShipped = invoice.WithShipping;
                ShippingMethode = invoice.ShippingMethod;
                ShippingAmont = invoice.ShippingAmount;
                ShippingTo = invoice.ShippingTo;
                SubTotal = invoice.SubTotal;
                Total = invoice.Total;
                
            }
        }
    }
}
