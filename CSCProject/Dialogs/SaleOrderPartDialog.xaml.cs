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
    public class SaleOrderPartDialogContext
    {
        public SaleOrderPart SaleOrderPart { get; set; }
        public List<SaleOrder> Orders { get; set; }
        public List<Part> Parts { get; set; }
        public List<Lot> Lots { get; set; }
    }

    /// <summary>
    /// Interaction logic for SaleOrderPartDialog.xaml
    /// </summary>
    public partial class SaleOrderPartDialog : Dialog
    {
        public SaleOrderPartDialog()
        {
            InitializeComponent();
        }

        protected override bool VerifyObject(object o)
        {
            return ((SaleOrderPart)o).LotId.Count() > 0 && ((SaleOrderPart)o).OrderId != -1 && ((SaleOrderPart)o).PartId != -1;
        }
    }
}
