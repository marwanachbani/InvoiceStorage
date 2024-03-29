using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.SqlServer.Data
{
    public class SqlServerCompanyInfo
    {
        public SqlServerCompanyInfo(string companyInfo, string contact, string email, string adress)
        {
            CompanyInfo = companyInfo;
            Contact = contact;
            Email = email;
            Adress = adress;
        }

        public int Id { get; set; }
        public string CompanyInfo { get; set; }
        public string Contact { get; set; }
        public string Email { get; set; }
        public string Adress { get; set; }
    }
}
