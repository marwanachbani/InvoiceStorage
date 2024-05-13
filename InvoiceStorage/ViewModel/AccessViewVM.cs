using CommunityToolkit.Mvvm.ComponentModel;
using Infra.Sqlite.Databases;
using InvoiceStorage.Events;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Controls;
using InvoiceStorage.Views;
using InvoiceStorage.Messages;

namespace InvoiceStorage.ViewModel
{
    public partial class AccessViewVM : ObservableRecipient
    {
        [ObservableProperty]
        private string userName;
        [ObservableProperty]
        private string password;
        private Databases database;
        [ObservableProperty]
        private string response;
        private UserControl userControl;
        
        public AccessViewVM()
        {
            Messenger.Register<AccessViewVM, DatabaseMessageSent>(this, (m, t) =>
            {
                SetDatabase(t.Value.Database);
            });
        }
        public void SetDatabase(Databases databases)
        {
            database = databases;
        }
        [RelayCommand]
        public void AccessToDatabase()
        {
            if(database.UserName == UserName &&  database.Password == Password)
            {
                userControl = new InvoiceDatabases();
                var model = new DatabaseToInvoiceDatabaseMessage(database);
                var @event = new DatabaseToInvoiceDatabaseMessageSent(model);
                var @eventUser = new ViewMessageSent(new Messages.ViewMessage(userControl));

                Messenger.Send(@event);
                Messenger.Send(@eventUser);

            }
            else
            {
                Response = "please try again your username and your password aren't correct yet "; 
            }
        }
    }
}
