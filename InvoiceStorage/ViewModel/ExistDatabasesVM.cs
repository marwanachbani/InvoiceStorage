using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Infra.Sqlite.Databases;
using InvoiceStorage.Events;
using InvoiceStorage.Messages;
using InvoiceStorage.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceStorage.ViewModel
{
    public partial class ExistDatabasesVM : ObservableRecipient
    {
        private readonly DatabaseHelpers _databasehelper = new DatabaseHelpers();
        [ObservableProperty]
        private ObservableCollection<Databases> databases = new ObservableCollection<Databases>() ;
        public ExistDatabasesVM()
        {
            databases = _databasehelper.GetAllDatabases();
        }
        [ObservableProperty]
        private Databases selectedDatbase;
        [ObservableProperty]
        private string response; 
        
        [RelayCommand]
        public void SendDatabase()
        {
            var database = _databasehelper.GetDatabaseByName(SelectedDatbase.DatabaseName);
            if(database == null)
            {
                Response = "No Database founf in name";
            }
            if(database != null)
            {
                var @event = new DatabaseMessageSent(new Messages.DatabaseMessage(database));
                var @viewevent = new ViewMessageSent(new Messages.ViewMessage(new AccessView()));
                Messenger.Send(@event);
                Messenger.Send(viewevent);
                SendDatabaseCom();

            }
        }
        private void SendDatabaseCom()
        {
            var model = new DatabaseToInvoiceDatabaseMessage(SelectedDatbase);
            var @event = new DatabaseToInvoiceDatabaseMessageSent(model);
            Messenger.Send(@event);
        }

    }
}
