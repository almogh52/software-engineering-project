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
    public class InventoryDialogContext
    {
        public Inventory Inventory { get; set; }
        public List<Lot> Lots { get; set; }
        public List<Part> Parts { get; set; }
        public List<Warehouse> Warehouses { get; set; }
    }

    /// <summary>
    /// Interaction logic for InventoryDialog.xaml
    /// </summary>
    public partial class InventoryDialog : Dialog
    {
        public InventoryDialog()
        {
            InitializeComponent();
        }

        protected override bool VerifyObject(object o)
        {
            return ((Inventory)o).LotId.Count() > 0 && ((Inventory)o).WarehouseId != -1 && ((Inventory)o).PartId != -1;
        }
    }
}
