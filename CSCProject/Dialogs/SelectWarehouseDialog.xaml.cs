﻿using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace CSCProject.Dialogs
{
    public class SelectWarehouseDialogContext
    {
        public int WarehouseId { get; set; } = -1;
        public List<Warehouse> Warehouses { get; set; }
    }

    /// <summary>
    /// Interaction logic for SelectWarehouseDialog.xaml
    /// </summary>
    public partial class SelectWarehouseDialog : Dialog
    {
        public SelectWarehouseDialog()
        {
            InitializeComponent();
        }

        protected override bool VerifyObject(object o)
        {
            return (int)o != -1;
        }
    }
}
