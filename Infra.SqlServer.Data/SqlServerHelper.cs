using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;

namespace Infra.SqlServer.Data
{
    public class SqlServerHelper
    {
        private readonly string _connectionString;
        private string _masterbdconnection;

        public SqlServerHelper(string connectionString)
        {
            _connectionString = connectionString;
        }

        public string CreateDatabase(string databasename, string server)
        {
            try
            {
                string masterConnectionString = $"Server={server};Database=master;Trusted_Connection=true;MultipleActiveResultSets=true;TrustServerCertificate=True";

                // Create the database if it doesn't exist
                using (SqlConnection masterConnection = new SqlConnection(masterConnectionString))
                {
                    masterConnection.Open();

                    using (SqlCommand createDbCommand = new SqlCommand($"IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = '{databasename}') CREATE DATABASE {databasename};", masterConnection))
                    {
                        createDbCommand.ExecuteNonQuery();
                    }

                    masterConnection.Close();
                }

                // Create tables if they don't exist
                string connectionString = $"Server={server};Database={databasename};Trusted_Connection=true;MultipleActiveResultSets=true;TrustServerCertificate=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand createTablesCommand = new SqlCommand(@"
                    CREATE TABLE  CompanyInfo (
                        Id INT PRIMARY KEY IDENTITY(1,1),
                        CompanyInfo NVARCHAR(255),
                        Contact NVARCHAR(255),
                        Email NVARCHAR(255),
                        Address NVARCHAR(255)
                    );

                    CREATE TABLE  Item (
                        Id UNIQUEIDENTIFIER PRIMARY KEY,
                        OrderId UNIQUEIDENTIFIER,
                        ProductName NVARCHAR(255),
                        Price DECIMAL(18, 2),
                        Quantity INT,
                        Total DECIMAL(18, 2)
                    );

                    CREATE TABLE OrderInvoice (
                        Id UNIQUEIDENTIFIER PRIMARY KEY,
                        InvoiceDate datetime ,
                        Customer NVARCHAR(255),
                        SubTotal DECIMAL(18, 2),
                        WithShipping BIT,
                        ShippingMethod NVARCHAR(255),
                        ShippingTo NVARCHAR(255),
                        ShippingAmount DECIMAL(18, 2),
                        TaxPercent DECIMAL(18, 2),
                        TaxAmount DECIMAL(18, 2),
                        ReductionPercent DECIMAL(18, 2),
                        ReductionAmount DECIMAL(18, 2),
                        Total DECIMAL(18, 2)
                    );
                ", connection))
                    {
                        createTablesCommand.ExecuteNonQuery();
                    }

                    connection.Close();
                }

                return "Database and tables created successfully.";
            }
            catch (Exception ex)
            {
                return $"Error creating database: {ex.Message}";
            }
        }


        // CRUD operations for CompanyInfo table
        public void AddCompanyInfo(SqlServerCompanyInfo companyInfo)
        {
            using (IDbConnection connection = new SqlConnection(_connectionString))
            {
                connection.Execute(@"INSERT INTO CompanyInfo (CompanyInfo, Contact, Email, Address)
                                     VALUES (@CompanyInfo, @Contact, @Email, @Adress);", companyInfo);
            }
        }

        public void UpdateCompanyInfo(SqlServerCompanyInfo companyInfo)
        {
            using (IDbConnection connection = new SqlConnection(_connectionString))
            {
                connection.Execute(@"UPDATE CompanyInfo
                                     SET CompanyInfo = @CompanyInfo,
                                         Contact = @Contact,
                                         Email = @Email,
                                         Address = @Address
                                     WHERE Id = @Id;", companyInfo);
            }
        }

        public void DeleteCompanyInfo(int companyId)
        {
            using (IDbConnection connection = new SqlConnection(_connectionString))
            {
                connection.Execute("DELETE FROM CompanyInfo WHERE Id = @Id;", new { Id = companyId });
            }
        }

        // CRUD operations for Item table
        public void AddItem(SqlServerItem item)
        {
            using (IDbConnection connection = new SqlConnection(_connectionString))
            {
                connection.Execute(@"INSERT INTO Item (Id, OrderId, ProductName, Price, Quantity, Total)
                                     VALUES (@Id, @OrderId, @ProductName, @Price, @Quantity, @Total);", item);
            }
        }

        // AddRangeItem method omitted for brevity

        public void AddInvoice(SqlServerOrderInvoice invoice)
        {
            using (IDbConnection connection = new SqlConnection(_connectionString))
            {
                connection.Execute(@"INSERT INTO OrderInvoice (Id, InvoiceDate ,Customer, SubTotal, WithShipping, ShippingMethod, ShippingTo, ShippingAmount, TaxPercent, TaxAmount, ReductionPercent, ReductionAmount, Total)
                                     VALUES (@Id,@Date ,@Customer, @SubTotal, @WithShipping, @ShippingMethod, @ShippingTo, @ShippingAmount, @TaxPercent, @TaxAmount, @ReductionPercent, @ReductionAmount, @Total);", invoice);
            }
        }

        public void UpdateInvoice(SqlServerOrderInvoice invoice)
        {
            using (IDbConnection connection = new SqlConnection(_connectionString))
            {
                connection.Execute(@"UPDATE OrderInvoice
                                     SET Customer = @Customer,
                                         DateInvoice = @Date,
                                         SubTotal = @SubTotal,
                                         WithShipping = @WithShipping,
                                         ShippingMethod = @ShippingMethod,
                                         ShippingTo = @ShippingTo,
                                         ShippingAmount = @ShippingAmount,
                                         TaxPercent = @TaxPercent,
                                         TaxAmount = @TaxAmount,
                                         ReductionPercent = @ReductionPercent,
                                         ReductionAmount = @ReductionAmount,
                                         Total = @Total
                                     WHERE Id = @Id;", invoice);
            }
        }

        public void DeleteInvoice(Guid invoiceId)
        {
            using (IDbConnection connection = new SqlConnection(_connectionString))
            {
                connection.Execute("DELETE FROM OrderInvoice WHERE Id = @Id;", new { Id = invoiceId });
            }
        }

        public ObservableCollection<SqlServerOrderInvoice> GetAllInvoices()
        {
            using (IDbConnection connection = new SqlConnection(_connectionString))
            {
                
                var list =  connection.Query<SqlServerOrderInvoice>("SELECT * FROM OrderInvoice;").ToList();
                var collec= new ObservableCollection<SqlServerOrderInvoice>();
                foreach (var item in list)
                {
                    collec.Add(item); 
                }
                return collec;
            }
        }

        public SqlServerOrderInvoice GetInvoiceById(Guid invoiceId)
        {
            using (IDbConnection connection = new SqlConnection(_connectionString))
            {
                return connection.QueryFirstOrDefault<SqlServerOrderInvoice>("SELECT * FROM OrderInvoice WHERE Id = @Id;", new { Id = invoiceId });
            }
        }
        public void AddRangeItem(List<SqlServerItem> items)
        {
            using (IDbConnection connection = new SqlConnection(_connectionString))
            {
                string insertSql = @"INSERT INTO Item (Id, OrderId, ProductName, Price, Quantity, Total)
                             VALUES (@Id, @OrderId, @ProductName, @Price, @Quantity, @Total);";

                connection.Execute(insertSql, items);
            }
        }
    }
}
