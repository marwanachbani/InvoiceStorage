using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceStorage.Messages
{
    public class CompanyInfo
    {
        public CompanyInfo(string companyName, string contact, string email, string address)
        {
            CompanyName = companyName;
            Contact = contact;
            Email = email;
            Address = address;
        }

        public string CompanyName { get; set; }
        public string Contact { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
    }
}
