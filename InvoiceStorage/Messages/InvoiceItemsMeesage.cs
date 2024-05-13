using InvoiceStorage.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceStorage.Messages
{
    public class InvoiceItemsMeesage
    {
        public ObservableCollection<Item> Items { get; set; }

        public InvoiceItemsMeesage(ObservableCollection<Item> items)
        {
            Items = items;
        }
    }
}
