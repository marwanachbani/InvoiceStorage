using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using InvoiceStorage.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceStorage.ViewModel
{
    public partial class CreateProjectVM : ObservableRecipient
    {
        [ObservableProperty]
        private string companyName;
        [ObservableProperty]
        private string telephoneNumber;
        [ObservableProperty]
        private string email;
        [ObservableProperty]
        private string address;
        [RelayCommand]
        public void SendComapnyInfo()
        {
            Messenger.Send(new CompanyInfoSent(new Messages.CompanyInfo(CompanyName,TelephoneNumber,Email,Address)));
        }
        

    }
}
