using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace CSCProject.ViewModels
{
    class PurchaseOrderPartsViewModel : DataTableViewModel<PurchaseOrderPart, DataHandlers.PurchaseOrderPartsDataHandler, Dialogs.PurchaseOrderPartDialog>
    {
        protected override List<Misc.Column> GetColumns()
        {
            return new List<Misc.Column> {
                new Misc.Column { Name = "Order Id", PropertyBinding = new Binding("OrderId"), AllowSearch = true },
                new Misc.Column { Name = "Part Id", PropertyBinding = new Binding("PartId"), AllowSearch = true },
                new Misc.Column { Name = "Part Description", PropertyBinding = new Binding("Part.Description"), AllowSearch = true },
                new Misc.Column { Name = "Quantity", PropertyBinding = new Binding("Quantity"), AllowSearch = true }
            };
        }

        protected override void InitDataItem(ref PurchaseOrderPart dataItem)
        {
            dataItem = new PurchaseOrderPart
            {
                OrderId = -1,
                PartId = -1
            };
        }

        protected override void InitDataItemDialog(ref Dialogs.PurchaseOrderPartDialog dialog, ref PurchaseOrderPart dataItem)
        {
            dialog = new Dialogs.PurchaseOrderPartDialog
            {
                DataContext = new Dialogs.PurchaseOrderPartDialogContext
                {
                    PurchaseOrderPart = dataItem,
                    Orders = dataHandler.GetEntities().PurchaseOrders.ToList().FindAll(type => !type.Deleted),
                    Parts = dataHandler.GetEntities().Parts.ToList().FindAll(type => !type.Deleted)
                }
            };
        }
    }
}
