﻿using InvoiceStorage.ViewModel;
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
    /// Interaction logic for InvoiceDatabaseDetails.xaml
    /// </summary>
    public partial class InvoiceDatabaseDetails : UserControl
    {
        public InvoiceDatabaseDetails()
        {
            DataContext = new InvoiceDatabaseDetailVM();
            InitializeComponent();
        }
    }
}
