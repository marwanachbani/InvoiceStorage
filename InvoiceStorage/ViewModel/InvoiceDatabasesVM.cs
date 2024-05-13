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
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace InvoiceStorage.ViewModel
{
    public partial class InvoiceDatabasesVM : ObservableRecipient
    {
        public InvoiceDatabasesVM()
        {
            Messenger.Register<InvoiceDatabasesVM, SelectedInvoiceMessageSent>(this, (m, t) =>
            {
                SelectInvoice(t.Value.Invoice);
            });

            Messenger.Register<InvoiceDatabasesVM, DatabaseToInvoiceDatabaseMessageSent>(this, (m, t) =>
            {
                SetDatabase(t.Value.Database);
            });
            
        }

        private  SqliteHelper _sqlite;
        private  SqlServerHelper _sqlSelver;
        
        private Databases database;
        [ObservableProperty]
        private UserControl leftSideContent;
        [ObservableProperty]
        private UserControl rightSideContent;
        [ObservableProperty]
        private Invoice selectedInvoice; 
        [RelayCommand]
        public void SwichtoAddInvoiceView()
        {
            var evnt = new DatabaseToInvoiceDatabaseMessageSent(new DatabaseToInvoiceDatabaseMessage(database));
           
            RightSideContent = new AddingInvoiceToDatabase();
            Messenger.Send(evnt);
        }
        public void SetDatabase(Databases  databaseparam)
        {
            database = databaseparam;
        }
        public  void RefreshLeftSide()
        {
            if(database.DatabaseInfra == "SqLite")
            {
                _sqlite = new SqliteHelper(database.DatabaseName);
                var invoices =  _sqlite.GetAllInvoices();
                if(invoices.Count == 0)
                {
                    LeftSideContent = new InvoiceDatabaseLeftSideForNoResult();
                    RightSideContent = new InvoiceDatabseRightSideHomeNoInvoice();
                }
                else
                {
                    
                    LeftSideContent = new InvoiceDbForLeftSideInvoicesList();
                    SendInvoicesOfSqlite(invoices);
                }
            }
            if(database.DatabaseInfra == "SqlServer")
            {
                _sqlSelver = new SqlServerHelper(database.ConnectionString);
                var invoices = _sqlSelver.GetAllInvoices();
                if (invoices.Count == 0)
                {
                    LeftSideContent = new InvoiceDatabaseLeftSideForNoResult();
                    RightSideContent = new InvoiceDatabseRightSideHomeNoInvoice();
                }
                else
                {
                    
                    LeftSideContent = new InvoiceDbForLeftSideInvoicesList();
                    SendInvoicesOfSqlService(invoices);
                }
            }
            
        }
        private void GetDatabase(Databases databases)
        {
            database = databases;
        }
        private void SendInvoicesOfSqlService(ObservableCollection<SqlServerOrderInvoice> items)
        {
            var model = new SqlServerInvoicesMessage(items);
            var message = new SqlServerInvoicesSent(model);
            Messenger.Send(message); 
        }
        private void SendInvoicesOfSqlite(ObservableCollection<SqliteOrderInvoice> items)
        {
            var model = new SqliteInvoicesMessage(items);
            var message = new SqliteInvoiceMessageSent(model);
            Messenger.Send(message); 
        }
        public void SelectInvoice(Invoice invoice)
        {
            if(invoice != null)
            {
                RightSideContent = new InvoiceDatabaseDetails();
                
            }
            else
            {
                RightSideContent = new InvoiceDatabaseHomeRightSide();
            }
               
        }

        
    }
}
