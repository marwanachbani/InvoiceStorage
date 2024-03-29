using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Data.Sql;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Data.Sql;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using Infra.Sqlite.Databases;
using Infra.SqlServer.Data;
using InfraSqlLite.Data;
using InvoiceStorage.Events;
using CommunityToolkit.Mvvm.Messaging;

namespace InvoiceStorage.ViewModel
{
    public partial class CreateDatabaseVM : ObservableRecipient
    {
        private readonly DatabaseHelpers _databasebridge = new DatabaseHelpers();
        
        public CreateDatabaseVM()
        {
            GetServers();
            LoadAuthentitcation();
            LoadDabaseInfra();
            Messenger.Register<CreateDatabaseVM, CompanyInfoSent>(this, (r, m) =>
            {
                CompanyName = m.Value.CompanyName;
                TelephoneNumber = m.Value.Contact;
                Email = m.Value.Email;
                Address = m.Value.Address;
            });
        }
        [ObservableProperty]
        private string databaseName;
        [ObservableProperty]
        private string userName;
        [ObservableProperty]
        private string password;
        [ObservableProperty]
        private string userNameSqlServer;
        [ObservableProperty]
        private string passwordSqlserver;
        [ObservableProperty]
        private bool includeItemsTable = false;
        [ObservableProperty]
        private string databaseInfrastructure;
        [ObservableProperty]
        private ObservableCollection<string> databaseInfrastructures = new ObservableCollection<string>() ;
        [ObservableProperty]
        private ObservableCollection<string> authentications = new ObservableCollection<string>();
        [ObservableProperty]
        private ObservableCollection<string> servers = new ObservableCollection<string>();
        [ObservableProperty]
        private string companyName;
        [ObservableProperty]
        private string telephoneNumber;
        [ObservableProperty]
        private string email;
        [ObservableProperty]
        private string address;
        [ObservableProperty]
        private string connectionString;
        [ObservableProperty]
        private string authentication;
        [ObservableProperty]
        private string server;
        
        private void GetServers()
        {
            var  serverTable = SqlDataSourceEnumerator.Instance.GetDataSources();

            // Create a list to store server names
            

            foreach (DataRow row in serverTable.Rows)
            {
                string serverName = row["ServerName"].ToString();
                string instanceName = row["InstanceName"].ToString();

                if (!string.IsNullOrEmpty(instanceName))
                {
                    Servers.Add(serverName + "\\" + instanceName);
                }
                else
                {
                    Servers.Add(serverName);
                }
            }
            
        }
        private void LoadDabaseInfra()
        {
            DatabaseInfrastructures.Add("SqLite");
            DatabaseInfrastructures.Add("SqlServer"); 
        }
        private void LoadAuthentitcation()
        {
            Authentications.Add("Windows Authentitcation");
            Authentications.Add("SqlSever Authentication"); 
        }
        [RelayCommand]
        public async Task CreateNewDatabase()
        {
            if(DatabaseInfrastructure == "SqLite")
            {
                var repo = new SqliteHelper(DatabaseName);
                repo.CreateDatabase();
                repo.AddCompanyInfo(new SqliteCompanyInfo(CompanyName,TelephoneNumber,Email,Address)); 
            }
            if(DatabaseInfrastructure == "SqlServer")
            {
                if(Authentication == "Windows Authentitcation")
                {
                    ConnectionString = $"Server={Server};Database={DatabaseName};Trusted_Connection=true;MultipleActiveResultSets=true;TrustServerCertificate=True";
                }
                if(Authentication == "SqlSever Authentication")
                {
                    ConnectionString = $"Server={Server};Database={DatabaseName};User Id={UserNameSqlServer};password={PasswordSqlserver};Trusted_Connection=False;MultipleActiveResultSets=true;TrustServerCertificate=True;";
                }
                var repo = new SqlServerHelper(ConnectionString);
                repo.CreateDatabase(DatabaseName,Server);
                repo.AddCompanyInfo(new SqlServerCompanyInfo(CompanyName, TelephoneNumber, Email, Address));
            }
            _databasebridge.AddDatabase(new Databases(DatabaseName, ConnectionString, DatabaseInfrastructure, UserName, Password));
            await Task.CompletedTask;
        }
        
    }
}
