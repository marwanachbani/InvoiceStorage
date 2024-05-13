using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using InvoiceStorage.Events;
using InvoiceStorage.Messages;
using InvoiceStorage.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceStorage.ViewModel
{
    public partial class DbProjectVM : ObservableRecipient
    {
        [RelayCommand]
        public void SwitchToCreateProject()
        {
            var view = new CreateProject();
            var message = new ViewMessage(view);
            var @event = new ViewMessageSent(message);
            Messenger.Send(@event); 


        }
        [RelayCommand]
        public void SwitchToExistDatabase()
        {
            var view = new ExistDatabases();
            var message = new ViewMessage(view);
            var @event = new ViewMessageSent(message);
            Messenger.Send(@event);
        }
    }
}
