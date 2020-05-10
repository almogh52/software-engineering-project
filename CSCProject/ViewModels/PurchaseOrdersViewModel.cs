using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace CSCProject.ViewModels
{
    class PurchaseOrdersViewModel : DataTableViewModel<PurchaseOrder, DataHandlers.PurchaseOrdersDataHandler, Dialogs.PurchaseOrderDialog>
    {
        public override bool ShowDataItemId { get; set; } = true;

        protected override List<Misc.Column> GetColumns()
        {
            return new List<Misc.Column> {
                new Misc.Column { Name = "Vendor Id", PropertyBinding = new Binding("VendorId"), AllowSearch = true },
                new Misc.Column { Name = "Vendor Name", PropertyBinding = new Binding("Vendor.Name"), AllowSearch = true },
                new Misc.Column { Name = "Order Date", PropertyBinding = new Binding("Date"), AllowSearch = true },
                new Misc.Column { Name = "Total Price", PropertyBinding = new Binding("Price") { StringFormat = "{0}₪" }, AllowSearch = true }
            };
        }

        protected override void InitDataItem(ref PurchaseOrder dataItem)
        {
            dataItem = new PurchaseOrder
            {
                VendorId = -1,
                Date = DateTime.Now
            };
        }

        protected override void InitDataItemDialog(ref Dialogs.PurchaseOrderDialog dialog, ref PurchaseOrder dataItem)
        {
            dialog = new Dialogs.PurchaseOrderDialog
            {
                DataContext = new Dialogs.PurchaseOrderDialogContext
                {
                    PurchaseOrder = dataItem,
                    Vendors = dataHandler.GetEntities().Vendors.ToList().FindAll(type => !type.Deleted)
                }
            };
        }
    }
}
