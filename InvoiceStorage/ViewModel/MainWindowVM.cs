using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Infra.Sqlite.Databases;
using InvoiceStorage.Events;
using InvoiceStorage.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace InvoiceStorage.ViewModel
{
    public partial class MainWindowVM : ObservableRecipient
    {
        public MainWindowVM()
        {
            Messenger.Register<MainWindowVM, CompanyInfoSent>(this, (m, t) =>
            {
                SwitchToCreateDatabase();
            });
        }
        [ObservableProperty]
        private UserControl pageContent = new CreateProject();

        [RelayCommand]
        public async Task SwitchtoHome()
        {
            PageContent = new Home();
            await Task.CompletedTask; 
        }
        [RelayCommand]
        public async Task SwitchToExport()
        {
            PageContent = new AddingInvoice();
            await Task.CompletedTask;
        }
        [RelayCommand]
        public async Task SwitchToProjectDb()
        {
            PageContent = new DBProject();
            await Task.CompletedTask;
        }
        public void SwitchToCreateProject()
        {
            PageContent = new CreateProject();
        }
        public void SwitchToCreateDatabase()
        {
            PageContent = new CreateDatabase();
        }
        
        
    }
}
