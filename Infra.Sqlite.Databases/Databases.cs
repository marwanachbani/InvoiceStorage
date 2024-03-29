using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Sqlite.Databases
{
    public  class Databases
    {
        public Databases(string databaseName, string connectionString, string databaseInfra, string userName, string password)
        {
            DatabaseName = databaseName;
            ConnectionString = connectionString;
            DatabaseInfra = databaseInfra;
            UserName = userName;
            Password = password;
        }

        public Guid Id { get; set; } = Guid.NewGuid();
        public string DatabaseName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseInfra { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
