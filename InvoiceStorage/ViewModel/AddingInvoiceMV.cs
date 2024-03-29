using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using CommunityToolkit;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using InvoiceStorage.Model;
using PDFCHARPConversion;
using PDFCHARPConversion.Models;
using WordConvertionNPoi;

namespace InvoiceStorage.ViewModel
{

    public partial class AddingInvoiceMV : ObservableRecipient
    {
        
        private PdfConversionHelper _pdfCOnversionHelper = new();
        private WordConversionHelper _wordConversionHelper = new();
        [ObservableProperty]
        private string customer;
        [ObservableProperty]
        private DateTime date;
        [ObservableProperty]
        private string productName;
        [ObservableProperty]
        private int quantity;
        [ObservableProperty]
        private decimal price;
        [ObservableProperty]
        private decimal taxPercent;
        [ObservableProperty]
        private decimal redcPercent;
        [ObservableProperty]
        private string shippingMethode;
        [ObservableProperty]
        private string shippingTo;
        [ObservableProperty]
        private decimal shippingAmont;
        [ObservableProperty]
        private bool isShipped = false;
        [ObservableProperty]
        private decimal subTotal;
        [ObservableProperty]
        private decimal total;
        [ObservableProperty]
        private string response;
        [ObservableProperty]
        private  ObservableCollection<Item>  itemsinvoice = new();
        [ObservableProperty]
        [NotifyPropertyChangedRecipients]

        private Item selectedItem;
       
        [RelayCommand]
        public async Task AddItemOnlist()
        {
            Itemsinvoice.Add(new Item(ProductName,Quantity,Price));
            SubTotal = 0;
            foreach(var item in Itemsinvoice)
            {
                
                SubTotal += item.Total;
                
            }
            var redc = SubTotal * RedcPercent / 100;
            var tax = SubTotal * TaxPercent / 100;
            Total = SubTotal + tax - redc;
            await Task.CompletedTask;
        }
        [RelayCommand]
        
        public void AllowShippingState()
        {
            IsShipped = true;
        }
        public void DisallowShipping()
        {
            IsShipped= false;
        }
        [RelayCommand]
        public async Task ConvertToPDF()
        {
            var redc = SubTotal * RedcPercent / 100;
            var tax = SubTotal * TaxPercent / 100;
            if (IsShipped==true)
            {
                var listship = new ObservableCollection<InvoiceItem>();
                foreach (var item in Itemsinvoice)
                {
                    listship.Add(new InvoiceItem(item.ProductName, item.Quantity, item.Price, item.Total));
                    var res = await _pdfCOnversionHelper.ConvertInputInvoiceToPdfWithShipping(Customer, Date, listship, TaxPercent, tax, RedcPercent, redc, SubTotal, ShippingMethode, ShippingTo, ShippingAmont);
                    Response = res;
                }
            }
            else
            {
                var list = new ObservableCollection<InvoiceItem>();
                foreach (var item in Itemsinvoice)
                {
                    list.Add(new InvoiceItem(item.ProductName, item.Quantity, item.Price, item.Total));
                }
                var response = await _pdfCOnversionHelper.ConvertInputInvoiceToPdfWithoutShipping(Customer, Date, list, TaxPercent, tax, RedcPercent, redc, Total);
                Response = response;
            }
            
        }
        [RelayCommand]
        public async Task ConvertToWord()
        {
            var redc = SubTotal * RedcPercent / 100;
            var tax = SubTotal * TaxPercent / 100;
            if (IsShipped == true)
            {
                var listship = new ObservableCollection<WordConvertionNPoi.Models.InvoiceItem>();
                foreach (var item in Itemsinvoice)
                {
                    listship.Add(new WordConvertionNPoi.Models.InvoiceItem(item.ProductName, item.Quantity, item.Price, item.Total));
                    var res = await _wordConversionHelper.ConvertInputInvoiceToWordWithShipping(Customer, Date, listship, TaxPercent, tax, RedcPercent, redc, SubTotal, ShippingMethode, ShippingTo, ShippingAmont);
                    Response = res;
                }
            }
            else
            {
                var list = new ObservableCollection<WordConvertionNPoi.Models.InvoiceItem>();
                foreach (var item in Itemsinvoice)
                {
                    list.Add(new WordConvertionNPoi.Models.InvoiceItem(item.ProductName, item.Quantity, item.Price, item.Total));
                }
                var response = await _wordConversionHelper.ConvertInputInvoiceToWordWithoutShipping(Customer, Date, list, TaxPercent, tax, RedcPercent, redc, Total);
                Response = response;
            }
        }
        [RelayCommand]
        [Obsolete]
        public async Task ConvertToExcel()
        {
            var redc = SubTotal * RedcPercent / 100;
            var tax = SubTotal * TaxPercent / 100;
            if (IsShipped == true)
            {
                var listship = new ObservableCollection<WordConvertionNPoi.Models.InvoiceItem>();
                foreach (var item in Itemsinvoice)
                {
                    listship.Add(new WordConvertionNPoi.Models.InvoiceItem(item.ProductName, item.Quantity, item.Price, item.Total));
                    var res = await _wordConversionHelper.ConvertInputInvoiceToExcelWithShipping(Customer, Date, listship, TaxPercent, tax, RedcPercent, redc, SubTotal, ShippingMethode, ShippingTo, ShippingAmont);
                    Response = res;
                }
            }
            else
            {
                var list = new ObservableCollection<WordConvertionNPoi.Models.InvoiceItem>();
                foreach (var item in Itemsinvoice)
                {
                    list.Add(new WordConvertionNPoi.Models.InvoiceItem(item.ProductName, item.Quantity, item.Price, item.Total));
                }
                var response = await _wordConversionHelper.ConvertInputInvoiceToExcelWithoutShipping(Customer, Date, list, TaxPercent, tax, RedcPercent, redc, Total);
                Response = response;
            }
        }
        private void Delete()
        {
            Itemsinvoice.Remove(SelectedItem);
        }
        [RelayCommand]
        public async Task DeleteItem()
        {
            Delete();
            await Task.CompletedTask;
            
        }
        [RelayCommand]
        public async Task EditItem()
        {
            await Task.CompletedTask; 
        }
        
    }
}
