using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.IO;

namespace Infra.Sqlite.Databases
{
    public class DatabaseHelpers
    {
        private readonly string _databasePath;
        private readonly string _connectionString;

        public DatabaseHelpers()
        {
            // Get the path to the folder where the application's executable is located
            string folderPath = AppDomain.CurrentDomain.BaseDirectory;

            // Combine the folder path with the database name to get the full database path
            _databasePath = Path.Combine(folderPath, "InvoiceStorageDbs.db");
            _connectionString = $"Data Source={_databasePath}";

            // Check if the database file exists, create it if it doesn't
            CheckAndCreateDb();
        }

        public void CheckAndCreateDb()
        {
            if (!File.Exists(_databasePath))
            {
                CreateDatabaseFile();
                CreateDatabaseTable();
            }
        }

        private void CreateDatabaseFile()
        {
            // Create an empty SQLite database file using standard file operations
            using (FileStream fs = File.Create(_databasePath))
            {
                // File is automatically closed and disposed after creation
            }
        }

        private void CreateDatabaseTable()
        {
            using (SqliteConnection connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                string createTableSql = @"CREATE TABLE IF NOT EXISTS DatabasesTable (
                                            Id TEXT PRIMARY KEY,
                                            DatabaseName TEXT,
                                            ConnectionString TEXT,
                                            DatabaseInfra TEXT,
                                            UserName TEXT,
                                            Password TEXT);";

                using (SqliteCommand command = new SqliteCommand(createTableSql, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        public void AddDatabase(Databases database)
        {
            using (SqliteConnection connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                string insertSql = @"INSERT INTO DatabasesTable (Id, DatabaseName, ConnectionString, DatabaseInfra, UserName, Password)
                                     VALUES (@Id, @DatabaseName, @ConnectionString, @DatabaseInfra, @UserName, @Password);";

                using (SqliteCommand command = new SqliteCommand(insertSql, connection))
                {
                    command.Parameters.AddWithValue("@Id", database.Id.ToString());
                    command.Parameters.AddWithValue("@DatabaseName", database.DatabaseName);
                    command.Parameters.AddWithValue("@ConnectionString", database.ConnectionString);
                    command.Parameters.AddWithValue("@DatabaseInfra", database.DatabaseInfra);
                    command.Parameters.AddWithValue("@UserName", database.UserName);
                    command.Parameters.AddWithValue("@Password", database.Password);

                    command.ExecuteNonQuery();
                }
            }
        }

        public List<Databases> GetAllDatabases()
        {
            List<Databases> databases = new List<Databases>();

            using (SqliteConnection connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                string selectSql = "SELECT * FROM DatabasesTable;";

                using (SqliteCommand command = new SqliteCommand(selectSql, connection))
                {
                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Databases database = new Databases
                            (
                                reader["DatabaseName"].ToString(),
                                reader["ConnectionString"].ToString(),
                                reader["DatabaseInfra"].ToString(),
                                reader["UserName"].ToString(),
                                reader["Password"].ToString()
                            );
                            databases.Add(database);
                        }
                    }
                }
            }

            return databases;
        }
    }
}