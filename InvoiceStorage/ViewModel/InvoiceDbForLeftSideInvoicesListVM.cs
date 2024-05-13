using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging; 
using Infra.SqlServer.Data;
using InfraSqlLite.Data;
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
    public partial class InvoiceDbForLeftSideInvoicesListVM : ObservableRecipient
    {
        
        [ObservableProperty]
        private ObservableCollection<Invoice> invoices = new ObservableCollection<Invoice>();
        [ObservableProperty]
        private Invoice selectedInvoice; 
        public InvoiceDbForLeftSideInvoicesListVM()
        {
            
            Messenger.Register<InvoiceDbForLeftSideInvoicesListVM, SqlServerInvoicesSent>(this, (t, m) =>
            {
                LoadItems(m.Value.Items); 
            });
            Messenger.Register<InvoiceDbForLeftSideInvoicesListVM, SqliteInvoiceMessageSent>(this, (t, m) =>
            {
                LoadItems(m.Value.Items);
            });
        }
        private void LoadItems(ObservableCollection<SqliteOrderInvoice> items )
        {
            Invoices.Clear();
            foreach (var item in items)
            {
                
                var products = new List<Item>();
                
                foreach (var product in item.Items)
                {
                    products.Add(new Item(product.ProductName , product.Quantity , product.Price ));
                }
                Invoices.Add(new Invoice(item.Id, item.Customer, products, item.SubTotal, item.WithShipping, item.ShippingMethod, item.ShippingTo, item.ShippingAmount, item.TaxPercent, item.TaxAmount, item.ReductionPercent, item.ReductionAmount, item.Total ,item.Date));
            }
        }
        private void LoadItems(ObservableCollection<SqlServerOrderInvoice>items )
        {
            Invoices.Clear(); 
            foreach (var item in items)
            {

                var products = new List<Item>();

                foreach (var product in item.Items)
                {
                    products.Add(new Item(product.ProductName, product.Quantity, product.Price));
                }
                Invoices.Add(new Invoice(item.Id, item.Customer, products, item.SubTotal, item.WithShipping, item.ShippingMethod, item.ShippingTo, item.ShippingAmount, item.TaxPercent, item.TaxAmount, item.ReductionPercent, item.ReductionAmount, item.Total,item.Date));
            }
        }
        public void SendSelectedInvoice()
        {
            var model = new SelectedInvoiceMessage(SelectedInvoice);
            var @event = new SelectedInvoiceMessageSent(model);
            var evnt = new SelelctedInvoiceToInvoicedeailsSent(model);
            Messenger.Send(@event);
            Messenger.Send(evnt);
        }
        
    }
}
