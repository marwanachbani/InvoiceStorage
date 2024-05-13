using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace InvoiceStorage.Messages
{
    public class ViewMessage
    {
        public ViewMessage(UserControl view)
        {
            View = view;
        }

        public UserControl View { get; set; }
    }
}
