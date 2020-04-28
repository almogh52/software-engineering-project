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
    public class PurchaseOrderPartDialogContext
    {
        public PurchaseOrderPart PurchaseOrderPart { get; set; }
        public List<PurchaseOrder> Orders { get; set; }
        public List<Part> Parts { get; set; }
        public List<Lot> Lots { get; set; }
    }

    /// <summary>
    /// Interaction logic for PurchaseOrderPartDialog.xaml
    /// </summary>
    public partial class PurchaseOrderPartDialog : Dialog
    {
        public PurchaseOrderPartDialog()
        {
            InitializeComponent();
        }

        protected override bool VerifyObject(object o)
        {
            return ((PurchaseOrderPart)o).LotId.Count() > 0 && ((PurchaseOrderPart)o).OrderId != -1 && ((PurchaseOrderPart)o).PartId != -1;
        }
    }
}
