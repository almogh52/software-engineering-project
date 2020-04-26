using MaterialDesignThemes.Wpf;
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
    public class SaleOrderDialogContext
    {
        public SaleOrder SaleOrder { get; set; }
        public List<Customer> Customers { get; set; }
    }

    /// <summary>
    /// Interaction logic for SaleOrderDialog.xaml
    /// </summary>
    public partial class SaleOrderDialog : Dialog
    {
        public SaleOrderDialog()
        {
            InitializeComponent();
        }

        protected override bool VerifyObject(object o)
        {
            return ((SaleOrder)o).CustomerId != -1;
        }
    }
}
