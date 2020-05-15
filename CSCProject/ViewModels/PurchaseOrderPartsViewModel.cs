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
        public override bool HasId { get; set; } = false;

        protected override List<Misc.Column> GetColumns()
        {
            return new List<Misc.Column> {
                new Misc.Column { Name = "Order Id", PropertyBinding = new Binding("OrderId"), AllowSearch = true },
                new Misc.Column { Name = "Part Id", PropertyBinding = new Binding("PartId"), AllowSearch = true },
                new Misc.Column { Name = "Part Description", PropertyBinding = new Binding("Part.Description"), AllowSearch = true },
                new Misc.Column { Name = "Lot Id", PropertyBinding = new Binding("LotId"), AllowSearch = true },
                new Misc.Column { Name = "Quantity", PropertyBinding = new MultiBinding() { Bindings = { new Binding("Quantity"), new Binding("Part.Unit") }, StringFormat="{0} {1}" }, AllowSearch = true }
            };
        }

        protected override void InitDataItem(ref PurchaseOrderPart dataItem)
        {
            dataItem = new PurchaseOrderPart
            {
                LotId = "",
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
                    Orders = dataHandler.GetEntities().PurchaseOrders.ToList().FindAll(order => !order.Deleted),
                    Parts = dataHandler.GetEntities().Parts.ToList().FindAll(part => !part.Deleted && part.Type == LotType.RawMaterial),
                    Lots = dataHandler.GetEntities().Lots.ToList().FindAll(lot => !lot.Deleted && lot.Type == LotType.RawMaterial)
                }
            };
        }
    }
}
