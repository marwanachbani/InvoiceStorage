using InvoiceStorage.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InvoiceStorage.Views
{
    /// <summary>
    /// Interaction logic for ExistDatabases.xaml
    /// </summary>
    public partial class ExistDatabases : UserControl
    {
        public ExistDatabases()
        {
            DataContext = new ExistDatabasesVM();
            InitializeComponent();
        }

        private void DataGridDatabase_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }
    }
}
