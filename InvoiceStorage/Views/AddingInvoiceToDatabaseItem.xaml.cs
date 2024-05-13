using InvoiceStorage.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for AddingInvoiceToDatabaseItem.xaml
    /// </summary>
    public partial class AddingInvoiceToDatabaseItem : UserControl
    {
        public AddingInvoiceToDatabaseItem()
        {
            InitializeComponent();
            DataContext = new AddingInvoicetoDatabaseItemsVM();
        }
        private async void DataGridInvoice_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {



            // Check if any item is selected in the DataGrid
            if (DataGridInvoice.SelectedItem != null)
            {
                // If an item is selected, hide the "Add item on list" button

                AddItemButton.Visibility = Visibility.Hidden;
                DeleteItem.Visibility = Visibility.Visible;
                await Task.CompletedTask;
            }
            else
            {
                AddItemButton.Visibility = Visibility.Visible;
                DeleteItem.Visibility = Visibility.Hidden;

            }

        }
        private void TextBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            // Only allow numeric input
            e.Handled = !IsTextNumeric(e.Text);
        }

        private bool IsTextNumeric(string text)
        {
            // Regular expression to match numeric characters
            Regex regex = new Regex("[^0-9]+");
            return !regex.IsMatch(text);
        }
        private void TextBoxPrice_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            string newText = textBox.Text.Insert(textBox.CaretIndex, e.Text);

            // Only allow numeric input and a single decimal point
            e.Handled = !IsTextValid(newText);
        }

        private bool IsTextValid(string text)
        {
            // Regular expression to match numeric characters and a single decimal point
            Regex regex = new Regex(@"^\d*\.?\d*$");
            return regex.IsMatch(text);
        }
        private void TextBoxPercent_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            // Only allow numeric input and a single decimal point
            e.Handled = !IsPercentTextValid(textBox.Text.Insert(textBox.SelectionStart, e.Text));
        }

        private bool IsPercentTextValid(string text)
        {
            // Regular expression to match numeric characters and a single decimal point
            Regex regex = new Regex(@"^\d*\.?\d*$");
            if (!regex.IsMatch(text))
                return false;

            // Parse the text to check if it's within the range of 0 to 100
            if (decimal.TryParse(text, out decimal value))
            {
                return value >= 0 && value <= 100;
            }

            return false;
        }
    }
}
