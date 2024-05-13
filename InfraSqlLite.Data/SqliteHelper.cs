using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraSqlLite.Data
{
    public class SqliteHelper
    {
        private readonly string _connectionString;

        public SqliteHelper(string databaseName)
        {
            // Specify the connection string using the database name
            _connectionString = $"Data Source={databaseName}.db";
        }

        public void CreateDatabase()
        {
            using (SqliteConnection connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                // Create tables if they don't exist
                CreateCompanyInfoTable(connection);
                CreateItemTable(connection);
                CreateOrderInvoiceTable(connection);
            }
        }

        private void CreateCompanyInfoTable(SqliteConnection connection)
        {
            string createTableSql = @"CREATE TABLE IF NOT EXISTS CompanyInfo (
                                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                    CompanyInfo TEXT,
                                    Contact TEXT,
                                    Email TEXT,
                                    Address TEXT);";

            using (SqliteCommand command = new SqliteCommand(createTableSql, connection))
            {
                command.ExecuteNonQuery();
            }
        }

        private void CreateItemTable(SqliteConnection connection)
        {
            string createTableSql = @"CREATE TABLE IF NOT EXISTS Item (
                                    Id TEXT PRIMARY KEY,
                                    OrderId TEXT,
                                    ProductName TEXT,
                                    Price DECIMAL,
                                    Quantity INTEGER,
                                    Total DECIMAL);";

            using (SqliteCommand command = new SqliteCommand(createTableSql, connection))
            {
                command.ExecuteNonQuery();
            }
        }

        private void CreateOrderInvoiceTable(SqliteConnection connection)
        {
            string createTableSql = @"CREATE TABLE IF NOT EXISTS OrderInvoice (
                                    Id TEXT PRIMARY KEY,
                                    InvoiceDate datetime ,
                                    Customer TEXT,
                                    SubTotal DECIMAL,
                                    WithShipping INTEGER,
                                    ShippingMethod TEXT,
                                    ShippingTo TEXT,
                                    ShippingAmount DECIMAL,
                                    TaxPercent DECIMAL,
                                    TaxAmount DECIMAL,
                                    ReductionPercent DECIMAL,
                                    ReductionAmount DECIMAL,
                                    Total DECIMAL);";

            using (SqliteCommand command = new SqliteCommand(createTableSql, connection))
            {
                command.ExecuteNonQuery();
            }
        }

        // CRUD operations for CompanyInfo table
        public void AddCompanyInfo(SqliteCompanyInfo companyInfo)
        {
            using (SqliteConnection connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                string insertSql = @"INSERT INTO CompanyInfo (CompanyInfo, Contact, Email, Address)
                                 VALUES (@CompanyInfo, @Contact, @Email, @Address);";

                using (SqliteCommand command = new SqliteCommand(insertSql, connection))
                {
                    command.Parameters.AddWithValue("@CompanyInfo", companyInfo.CompanyInfo);
                    command.Parameters.AddWithValue("@Contact", companyInfo.Contact);
                    command.Parameters.AddWithValue("@Email", companyInfo.Email);
                    command.Parameters.AddWithValue("@Address", companyInfo.Adress);

                    command.ExecuteNonQuery();
                }
            }
        }

        public void UpdateCompanyInfo(SqliteCompanyInfo companyInfo)
        {
            using (SqliteConnection connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                string updateSql = @"UPDATE CompanyInfo
                                 SET CompanyInfo = @CompanyInfo,
                                     Contact = @Contact,
                                     Email = @Email,
                                     Address = @Address
                                 WHERE Id = @Id;";

                using (SqliteCommand command = new SqliteCommand(updateSql, connection))
                {
                    command.Parameters.AddWithValue("@CompanyInfo", companyInfo.CompanyInfo);
                    command.Parameters.AddWithValue("@Contact", companyInfo.Contact);
                    command.Parameters.AddWithValue("@Email", companyInfo.Email);
                    command.Parameters.AddWithValue("@Address", companyInfo.Adress);
                    command.Parameters.AddWithValue("@Id", companyInfo.Id);

                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteCompanyInfo(int companyId)
        {
            using (SqliteConnection connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                string deleteSql = @"DELETE FROM CompanyInfo WHERE Id = @Id;";

                using (SqliteCommand command = new SqliteCommand(deleteSql, connection))
                {
                    command.Parameters.AddWithValue("@Id", companyId);

                    command.ExecuteNonQuery();
                }
            }
        }

        // CRUD operations for Item table
        public void AddItem(SqliteItem item)
        {
            using (SqliteConnection connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                string insertSql = @"INSERT INTO Item (Id, OrderId, ProductName, Price, Quantity, Total)
                                 VALUES (@Id, @OrderId, @ProductName, @Price, @Quantity, @Total);";

                using (SqliteCommand command = new SqliteCommand(insertSql, connection))
                {
                    command.Parameters.AddWithValue("@Id", item.Id.ToString());
                    command.Parameters.AddWithValue("@OrderId", item.OrderId.ToString());
                    command.Parameters.AddWithValue("@ProductName", item.ProductName);
                    command.Parameters.AddWithValue("@Price", item.Price);
                    command.Parameters.AddWithValue("@Quantity", item.Quantity);
                    command.Parameters.AddWithValue("@Total", item.Total);

                    command.ExecuteNonQuery();
                }
            }
        }
        public void AddRangeItem(List<SqliteItem> items)
        {
            using (SqliteConnection connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                string insertSql = @"INSERT INTO Item (Id, OrderId, ProductName, Price, Quantity, Total)
                             VALUES (@Id, @OrderId, @ProductName, @Price, @Quantity, @Total);";

                using (SqliteCommand command = new SqliteCommand(insertSql, connection))
                {
                    foreach (var item in items)
                    {
                        command.Parameters.Clear();
                        command.Parameters.AddWithValue("@Id", item.Id.ToString());
                        command.Parameters.AddWithValue("@OrderId", item.OrderId.ToString());
                        command.Parameters.AddWithValue("@ProductName", item.ProductName);
                        command.Parameters.AddWithValue("@Price", item.Price);
                        command.Parameters.AddWithValue("@Quantity", item.Quantity);
                        command.Parameters.AddWithValue("@Total", item.Total);

                        command.ExecuteNonQuery();
                    }
                }
            }
        }
        public void AddInvoice(SqliteOrderInvoice invoice)
        {
            using (SqliteConnection connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                string insertSql = @"INSERT INTO OrderInvoice (Id,DateInvoice, Customer, SubTotal, WithShipping, ShippingMethod, ShippingTo, ShippingAmount, TaxPercent, TaxAmount, ReductionPercent, ReductionAmount, Total)
                             VALUES (@Id, @Customer,@Date, @SubTotal, @WithShipping, @ShippingMethod, @ShippingTo, @ShippingAmount, @TaxPercent, @TaxAmount, @ReductionPercent, @ReductionAmount, @Total);";

                using (SqliteCommand command = new SqliteCommand(insertSql, connection))
                {
                    command.Parameters.AddWithValue("@Id", invoice.Id.ToString());

                    command.Parameters.AddWithValue("@Customer", invoice.Customer);
                    command.Parameters.AddWithValue("@Date", invoice.Date);
                    command.Parameters.AddWithValue("@SubTotal", invoice.SubTotal);
                    command.Parameters.AddWithValue("@WithShipping", invoice.WithShipping);
                    command.Parameters.AddWithValue("@ShippingMethod", invoice.ShippingMethod);
                    command.Parameters.AddWithValue("@ShippingTo", invoice.ShippingTo);
                    command.Parameters.AddWithValue("@ShippingAmount", invoice.ShippingAmount);
                    command.Parameters.AddWithValue("@TaxPercent", invoice.TaxPercent);
                    command.Parameters.AddWithValue("@TaxAmount", invoice.TaxAmount);
                    command.Parameters.AddWithValue("@ReductionPercent", invoice.ReductionPercent);
                    command.Parameters.AddWithValue("@ReductionAmount", invoice.ReductionAmount);
                    command.Parameters.AddWithValue("@Total", invoice.Total);

                    command.ExecuteNonQuery();
                }
            }
        }

        public void UpdateInvoice(SqliteOrderInvoice invoice)
        {
            using (SqliteConnection connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                string updateSql = @"UPDATE OrderInvoice
                             SET Customer = @Customer,
                                 InvoiceDate = @Date ,
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
                             WHERE Id = @Id;";

                using (SqliteCommand command = new SqliteCommand(updateSql, connection))
                {
                    command.Parameters.AddWithValue("@Customer", invoice.Customer);
                    command.Parameters.AddWithValue("@Date", invoice.Date);
                    command.Parameters.AddWithValue("@SubTotal", invoice.SubTotal);
                    command.Parameters.AddWithValue("@WithShipping", invoice.WithShipping);
                    command.Parameters.AddWithValue("@ShippingMethod", invoice.ShippingMethod);
                    command.Parameters.AddWithValue("@ShippingTo", invoice.ShippingTo);
                    command.Parameters.AddWithValue("@ShippingAmount", invoice.ShippingAmount);
                    command.Parameters.AddWithValue("@TaxPercent", invoice.TaxPercent);
                    command.Parameters.AddWithValue("@TaxAmount", invoice.TaxAmount);
                    command.Parameters.AddWithValue("@ReductionPercent", invoice.ReductionPercent);
                    command.Parameters.AddWithValue("@ReductionAmount", invoice.ReductionAmount);
                    command.Parameters.AddWithValue("@Total", invoice.Total);
                    command.Parameters.AddWithValue("@Id", invoice.Id.ToString());

                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteInvoice(Guid invoiceId)
        {
            using (SqliteConnection connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                string deleteSql = @"DELETE FROM OrderInvoice WHERE Id = @Id;";

                using (SqliteCommand command = new SqliteCommand(deleteSql, connection))
                {
                    command.Parameters.AddWithValue("@Id", invoiceId.ToString());

                    command.ExecuteNonQuery();
                }
            }
        }

        public ObservableCollection<SqliteOrderInvoice> GetAllInvoices()
        {
            ObservableCollection<SqliteOrderInvoice> invoices = new ObservableCollection<SqliteOrderInvoice>();

            using (SqliteConnection connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                string selectSql = "SELECT * FROM OrderInvoice;";

                using (SqliteCommand command = new SqliteCommand(selectSql, connection))
                {
                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            SqliteOrderInvoice invoice = new SqliteOrderInvoice
                            {
                                Id = Guid.Parse(reader["Id"].ToString()),
                                Date = Convert.ToDateTime(reader["Date"]),
                                Customer = reader["Customer"].ToString(),
                                SubTotal = Convert.ToDecimal(reader["SubTotal"]),
                                WithShipping = Convert.ToBoolean(reader["WithShipping"]),
                                ShippingMethod = reader["ShippingMethod"].ToString(),
                                ShippingTo = reader["ShippingTo"].ToString(),
                                ShippingAmount = Convert.ToDecimal(reader["ShippingAmount"]),
                                TaxPercent = Convert.ToDecimal(reader["TaxPercent"]),
                                TaxAmount = Convert.ToDecimal(reader["TaxAmount"]),
                                ReductionPercent = Convert.ToDecimal(reader["ReductionPercent"]),
                                ReductionAmount = Convert.ToDecimal(reader["ReductionAmount"]),
                                Total = Convert.ToDecimal(reader["Total"])
                            };
                            invoices.Add(invoice);
                        }
                    }
                }
            }

            return invoices;
        }



        public SqliteOrderInvoice GetInvoiceById(Guid invoiceId)
        {
            using (SqliteConnection connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                string selectSql = "SELECT * FROM OrderInvoice WHERE Id = @Id;";

                using (SqliteCommand command = new SqliteCommand(selectSql, connection))
                {
                    command.Parameters.AddWithValue("@Id", invoiceId.ToString());

                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            SqliteOrderInvoice invoice = new SqliteOrderInvoice
                            {
                                Id = Guid.Parse(reader["Id"].ToString()),
                                Date = Convert.ToDateTime(reader["Date"]),
                                Customer = reader["Customer"].ToString(),
                                SubTotal = Convert.ToDecimal(reader["SubTotal"]),
                                WithShipping = Convert.ToBoolean(reader["WithShipping"]),
                                ShippingMethod = reader["ShippingMethod"].ToString(),
                                ShippingTo = reader["ShippingTo"].ToString(),
                                ShippingAmount = Convert.ToDecimal(reader["ShippingAmount"]),
                                TaxPercent = Convert.ToDecimal(reader["TaxPercent"]),
                                TaxAmount = Convert.ToDecimal(reader["TaxAmount"]),
                                ReductionPercent = Convert.ToDecimal(reader["ReductionPercent"]),
                                ReductionAmount = Convert.ToDecimal(reader["ReductionAmount"]),
                                Total = Convert.ToDecimal(reader["Total"])
                            };
                            return invoice;
                        }
                    }
                }
            }

            return null; // Return null if the invoice with the specified ID is not found
        }

    }
}
