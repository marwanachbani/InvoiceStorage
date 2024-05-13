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
    /// Interaction logic for AccessView.xaml
    /// </summary>
    public partial class AccessView : UserControl
    {
        public static readonly DependencyProperty PasswordProperty =
    DependencyProperty.Register("Password", typeof(string), typeof(AccessView), new PropertyMetadata(""));

        public string Password
        {
            get { return (string)GetValue(PasswordProperty); }
            set { SetValue(PasswordProperty, value); }
        }

        public AccessView()
        {
            InitializeComponent();
            DataContext = new AccessViewVM();

        }
        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is AccessViewVM viewModel)
            {
                if (sender is PasswordBox passwordBox)
                {
                    viewModel.Password = passwordBox.Password;
                }
            }
        }
    }
}
