using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Infra.Sqlite.Databases;
using Infra.SqlServer.Data;
using InfraSqlLite.Data;
using InvoiceStorage.Events;
using InvoiceStorage.Messages;
using InvoiceStorage.Model;
using InvoiceStorage.Views;
using PDFCHARPConversion;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using WordConvertionNPoi;

namespace InvoiceStorage.ViewModel
{
    public partial class AddingInvoiceToDatabaseVM : ObservableRecipient
    {
        
        public AddingInvoiceToDatabaseVM()
        {
            IsInfo = "Red";
            IsItems = "White"; 
            IsTaxRedc = "White";
            Itemsinvoice.Clear();
            Messenger.Register<AddingInvoiceToDatabaseVM, DatabaseToInvoiceDatabaseMessageSent>(this, (t, r) =>
            {
                database = r.Value.Database;
            });
            Messenger.Register<AddingInvoiceToDatabaseVM,InvoiceInfoMessageSent>(this , (r,t)=>
            {
                AddInfo(t.Value.Customer, t.Value.Date);
            });
            Messenger.Register<ItemsPreviousClicked>(this, (t, r) => { onPreviousAddInfo(); }
            );
            Messenger.Register<AddingInvoiceToDatabaseVM, InvoiceItemsMessageSent>(this, (t, r) =>
            {
                AddItems(r.Value.Items);
            });
            Messenger.Register<TaxRedPreviousClicked>(this, (r, t) =>
            {
                onPreviousAddTax();
            });
            Messenger.Register<AddingInvoiceToDatabaseVM, TaxRedMessageSent>(this, (r, t) =>
            {
                AddRestCalc(t.Value.TaxPercent, t.Value.RedcPercent);
                AddInvoice(database);
            });
            Messenger.Register<AddingInvoiceToDatabaseVM, TaxRedcShipMessageSent>(this, (r, t) =>
            {
                AddRestCalc(t.Value.TaxPercent, t.Value.RedcPercent);
                AddShipping(t.Value.ShippingMethod, t.Value.ShippingTo, t.Value.ShippingAmount);
                AddInvoice(database);
            });
            
        }
        private Databases database;
        [ObservableProperty]
        private string isInfo;
        [ObservableProperty]
        private string isItems;
        [ObservableProperty]
        private  string    isTaxRedc;
       
        [ObservableProperty]
        private string customer ="";
        [ObservableProperty]
        private DateTime date =DateTime.Now;
        
        [ObservableProperty]
        private decimal taxPercent = 0;
        [ObservableProperty]
        private decimal redcPercent = 0;
        [ObservableProperty]
        private string shippingMethode="";
        [ObservableProperty]
        private string shippingTo = "";
        [ObservableProperty]
        private decimal shippingAmont = 0;
        [ObservableProperty]
        private bool isShipped = false;
        [ObservableProperty]
        private decimal subTotal = 0;
        [ObservableProperty]
        private decimal total = 0;
        [ObservableProperty]
        private string response = "";
        [ObservableProperty]
        private ObservableCollection<Item> itemsinvoice = new();
        [ObservableProperty]
        [NotifyPropertyChangedRecipients]

        private Item selectedItem = null;
        [ObservableProperty]
        private UserControl userControl = new AddinginvoiceToDatabaseInfo();
        
        public void Switchtoitemview()
        {
            UserControl = new AddingInvoiceToDatabaseItem();
            IsInfo = "White";
            IsItems = "Red";
            IsTaxRedc = "White";
        }
        
        public void SwitchtoRectTaxView()
        {
            UserControl = new AddingInvoiceToDatabaseTaxReduction();
            IsInfo = "White";
            IsItems = "White";
            IsTaxRedc = "Red";
        }
        private void AddInfo(string customer , DateTime date)
        {
            Customer = customer;
            Date = date;
            Switchtoitemview();
        }
        private void AddItems(ObservableCollection<Item> itemsinvoice)
        {
            Itemsinvoice = itemsinvoice;
            SwitchtoRectTaxView();
        }
        private void AddRestCalc(decimal taxpercent , decimal redcpecent)
        {
            TaxPercent = taxpercent;
            RedcPercent = redcpecent;   

        }
        private void AddShipping(string shippingMethod , string shippingTo , decimal shippinAmount)
        {
            ShippingMethode = shippingMethod; ShippingTo = shippingTo; ShippingAmont = shippinAmount;
        }
        private void onPreviousAddInfo()
        {
            UserControl = new AddinginvoiceToDatabaseInfo();
            IsInfo = "Red";
            IsItems = "White";
            IsTaxRedc = "White";
        }
        private void onPreviousAddTax()
        {
            UserControl = new AddingInvoiceToDatabaseItem();
            IsInfo = "White";
            IsItems = "Red";
            IsTaxRedc = "White";
        }
        private void AddInvoice(Databases database)
        {

            if (database.DatabaseInfra == "SqLite")
            {
                
            }
            if (database.DatabaseInfra == "SqlServer")
            {
                var sql = new SqlServerHelper(database.ConnectionString);
                var id = Guid.NewGuid();
                
                foreach (var item in Itemsinvoice)
                {
                    SubTotal += item.Total; 
                }
                decimal taxamount = SubTotal * TaxPercent / 100;
                decimal redamount = SubTotal * RedcPercent /100;
                decimal total = SubTotal + taxamount + ShippingAmont - redamount;
                sql.AddInvoice(new SqlServerOrderInvoice(id,Customer,Date,SubTotal,IsShipped,ShippingMethode,ShippingTo,ShippingAmont,TaxPercent,taxamount,RedcPercent,redamount,total));
                foreach (var item in Itemsinvoice)
                {
                    sql.AddItem(new SqlServerItem(id, item.ProductName, item.Price, item.Quantity, item.Total));
                }

            }
        }

    }
}
